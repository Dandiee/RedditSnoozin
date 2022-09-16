using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlizzCheck.Extensions;
using BlizzCheck.Models;
using BlizzCheck.Models.CharProfile;
using BlizzCheck.Models.Mdi;
using BlizzCheck.Models.Pets;
using Refit;

namespace BlizzCheck
{
    public sealed class App2
    {
        private readonly IBlizzClient _client;
        private readonly IRawData _rawData;
        private readonly CacheService _cacheService;
        private static readonly DateTimeOffset StartDate = new DateTimeOffset(2022, 6, 10, 0, 0, 0, TimeSpan.Zero);

        public static readonly IReadOnlySet<string> Subreddits =
            new[] { "doors", "plantclinic", "blenderhelp", "childfree", "somethingimade" }.ToHashSet(StringComparer.OrdinalIgnoreCase);

        public App2(IBlizzClient client, IRawData rawData, CacheService cacheService)
        {
            _client = client;
            _rawData = rawData;
            _cacheService = cacheService;
        }


        public async Task Run()
        {
            await WarmUp();

            foreach (var subreddit in Subreddits)
            {
                await ProcessSubreddit(subreddit);
            }
        }

        private async Task WarmUp()
        {
            var twoDaysAgo = DateTimeOffset.Now.AddDays(-2).ToUnixTimeSeconds();

            foreach (var subreddit in Subreddits)
            {
                var savedSubmitsRaw = _cacheService.GetData(subreddit);
                var savedSubmits =
                    string.IsNullOrEmpty(savedSubmitsRaw)
                        ? new List<RawSubmit>()
                        : JsonSerializer.Deserialize<List<RawSubmit>>(savedSubmitsRaw);

                var lastSubmits = savedSubmits.Where(s => s.created_utc >= twoDaysAgo).ToList();
                foreach (var lastSubmit in lastSubmits)
                {
                    await ProcessSubmit(subreddit, lastSubmit, true);
                }
            }
        }

        public async Task ProcessSubreddit(string subreddit)
        {
            var savedSubmitsRaw = _cacheService.GetData(subreddit);
            var savedSubmits =
                string.IsNullOrEmpty(savedSubmitsRaw)
                    ? new List<RawSubmit>()
                    : JsonSerializer.Deserialize<List<RawSubmit>>(savedSubmitsRaw);

            var orderedSavedSubmits = savedSubmits.OrderByDescending(s => s.created_utc).ToList();
            var lastSavedSubmit = orderedSavedSubmits.FirstOrDefault();
            var lastSubmit = lastSavedSubmit?.created_utc ?? StartDate.ToUnixTimeSeconds();

            while (true)
            {
                var submitsResponse = await _rawData.GetSubmitRoot(subreddit, lastSubmit);
                if ((submitsResponse?.data?.Length ?? 0) == 0)
                {
                    break;
                }

                foreach (var rawSubmit in submitsResponse.data)
                {
                    await ProcessSubmit(subreddit, rawSubmit);
                }

                savedSubmits.AddRange(submitsResponse.data);

                _cacheService.SetData(subreddit, savedSubmits.OrderByDescending(s => s.created_utc).ToList());
                Console.WriteLine($"{subreddit} fetched from {DateTimeOffset.FromUnixTimeSeconds(lastSubmit)}");

                lastSubmit = submitsResponse.data.OrderByDescending(s => s.created_utc).First().created_utc;
                Console.WriteLine($"{subreddit} start fetching from {DateTimeOffset.FromUnixTimeSeconds(lastSubmit)}");
            }
        }

        private async Task ProcessSubmit(string subreddit, RawSubmit rawSubmit, bool forceFetch = false)
        {
            var commentsKey = $"{subreddit}_{rawSubmit.id}";
            var cachedComments = _cacheService.GetData(commentsKey);
            if (cachedComments != null && !forceFetch)
            {
                return;
            }

            var authors = new HashSet<string> { rawSubmit.author };

            var commentsRaw = await _client.GetCommentsRaw(subreddit, rawSubmit.id);
            var comments = JsonSerializer.Deserialize<CommentBase[]>(commentsRaw);

            var queue = new Queue<CommentBase>(comments);
            while (queue.Count > 0)
            {
                var next = queue.Dequeue();
                if (next?.data?.children != null)
                {
                    foreach (var child in next.data.children)
                    {
                        authors.Add(child.data.author);
                        if (child.data.replies != null)
                        {
                            queue.Enqueue(child.data.replies);
                        }
                    }
                }
            }

            await ProcessAuthors(authors);
            _cacheService.SetData(commentsKey, commentsRaw);
            Console.WriteLine($"Submits processed");
        }

        private async Task ProcessAuthors(HashSet<string> authors)
        {
            var authorsKey = "authors";

            var savedAuthorsRaw = _cacheService.GetData(authorsKey);
            var savedAuthors =
                string.IsNullOrEmpty(savedAuthorsRaw)
                    ? new List<UserRoot>()
                    : JsonSerializer.Deserialize<List<UserRoot>>(savedAuthorsRaw);

            var hash = savedAuthors.Select(s => s.data.name).ToHashSet();
            var missingUsers = authors.Where(a => !hash.Contains(a)).ToList();

            foreach (var missingUserName in missingUsers)
            {
                if (missingUserName == "[deleted]")
                {
                    continue;
                }

                try
                {
                    var userObj = await _client.GetUserRoot(missingUserName);
                    savedAuthors.Add(userObj);
                    Console.WriteLine($"User logged: {userObj.data.name}");
                }
                catch
                {
                    Console.WriteLine("User is missing: " + missingUserName);
                }
            }

            _cacheService.SetData(authorsKey, savedAuthors);
        }

    }

}

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
    public sealed class App
    {
        public const string CacheRoot = "d:/zizicache";
        private readonly IBlizzClient _client;
        private readonly IRawData _rawData;
        private const string DataRoot = @"c:\Projects\Data\Reddit1\";
        private static readonly DateTimeOffset StartDate = new DateTimeOffset(2022, 6, 10, 0, 0, 0, TimeSpan.Zero);
        public static readonly IReadOnlySet<string> Subs =
            new[] { "plantclinic", "blenderhelp" }.ToHashSet(StringComparer.OrdinalIgnoreCase);

        public App(IBlizzClient client, IRawData rawData)
        {
            _client = client;
            _rawData = rawData;
        }

        private async Task ProcessSub(string sub)
        {
            while (true)
            {
                var filePath = $"{DataRoot}\\Submits\\{sub}.dat";

                var after = GetLastFetchedTimeStamp(sub);
                Console.WriteLine(DateTimeOffset.FromUnixTimeSeconds(after));
                var data = await _rawData.GetSubmitRoot(sub, after);
                //var dataRaw = await _rawData.GetSubmitRootRaw(sub, after);
                if ((data?.data?.Length ?? 0) == 0)
                {
                    break;
                }

                foreach (var submit in data.data)
                {
                    await ProcessSubmits(submit.id);
                }

              
                var sofar = TryReadCache<RawSubmit>(filePath);
                sofar.AddRange(data.data);
                var json = JsonSerializer.Serialize(sofar);
                json.WriteFile(filePath);
            }
        }



        private long GetLastFetchedTimeStamp(string sub)
        {
            var filePath = $"{DataRoot}\\Submits\\{sub}.dat";
            var data = TryReadCache<RawSubmit>(filePath);
            return data.OrderByDescending(s => s.created_utc).FirstOrDefault()?.created_utc ?? StartDate.ToUnixTimeSeconds();
        }

        private List<T> TryReadCache<T>(string filePath)
        {
            var fi = new FileInfo(filePath);
            if (fi.Exists)
            {
                var text = File.ReadAllText(filePath);
                var data = JsonSerializer.Deserialize<List<T>>(text);

                return data;
            }

            return new List<T>();
        }

        private async Task ProcessSubmits(string sub)
        {
            var submits = TryReadCache<RawSubmit>($"{DataRoot}\\Submits\\{sub}.dat");
            var authors = submits.Select(s => s.author).ToHashSet();
            var users = TryReadCache<UserRoot>($"{DataRoot}\\users.dat");
            var fetchedUsers = users.Select(s => s.data.name).ToHashSet();

            foreach (var submit in submits)
            {
                var q = await _client.GetComments(sub, submit.id);
                var json = JsonSerializer.Serialize(q);
                await File.WriteAllTextAsync($"{DataRoot}\\Submits\\{sub}\\comments\\{submit.id}.dat", json);

                var queue = new Queue<CommentBase>(q);
                while (queue.Count > 0)
                {
                    var next = queue.Dequeue();
                    foreach (var child in next.data.children)
                    {
                        authors.Add(child.data.author);
                        queue.Enqueue(child.data.replies);
                    }
                }
            }

            foreach (var author in authors.Where(a => !fetchedUsers.Contains(a)))
            {
                await ProcessUser(author);
            }
        }

        private async Task ProcessUser(string user)
        {
            var path = $"{DataRoot}\\users.dat";
            var users = TryReadCache<UserRoot>(path);
            if (users.Any(s => s.data.name == user))
            {
                Console.WriteLine($"FOUND: {user}");
                return;
            }

            try
            {
                var userObj = await _client.GetUserRoot(user);
                users.Add(userObj);

                var json = JsonSerializer.Serialize(users);
                json.WriteFile(path);
                Console.WriteLine($"SAVED: {user}");
            }
            catch (Exception e)
            {
                File.AppendAllText($"{DataRoot}\\users_error.dat", $"\r\n{user}");
                Console.WriteLine($"ERROR: {user}");
            }
            
        }

        public async Task Run()
        {
            foreach (var sub in Subs)
            {
                await ProcessSub(sub);
            }

            

            return;
            var q = File.ReadAllLines("D:\\user_created3.dat").Where(s => !string.IsNullOrEmpty(s))
                .Select(s =>
                {
                    var data = s.Split('\t');
                    var floatDate = float.Parse(data[1]);
                    var intdate = (int)floatDate;
                    var realDate = DateTimeOffset.FromUnixTimeSeconds(intdate);

                    return new
                    {
                        Name = data[0],
                        Joined = realDate
                    };

                }).Where(s => s.Joined >= new DateTimeOffset(2022, 06, 10, 0, 0, 0, TimeSpan.Zero))
                .OrderBy(s => s.Joined)
                .ToList();


            foreach (var item in q)
            {
                Console.WriteLine($"https://www.reddit.com/user/{item.Name}");
            }

        }

        private async Task GetNames()
        {
            // await FetchNetwork();

            var rawSubmits = JsonSerializer.Deserialize<List<RawSubmit>>(File.ReadAllText("D:\\sokadat.adat"));

            var authors = rawSubmits.Select(s => s.author).ToHashSet();

            Dictionary<string, DateTimeOffset> nameOffsets = new Dictionary<string, DateTimeOffset>();



            foreach (var author in authors)
            {
                try
                {
                    var q = await _client.GetUserRoot(author);
                    nameOffsets[author] = q.data.CreatedDt;
                    Console.WriteLine($"{author}\t{q.data.CreatedDt}");
                    File.AppendAllText("D:\\user_created3.dat", $"{author}\t{q.data.created_utc}\t{q.data.CreatedDt}\r\n");
                }
                catch
                {
                    Console.WriteLine("ERROR: " + author);
                    File.AppendAllText("D:\\user_created_errors.dat", $"{author}\r\n");
                }
            }




            //var fullNames = rawSubmits.Select(s => s.author_fullname).ToHashSet();

            foreach (var submit in rawSubmits)
            {
                if (submit.author.Contains("92", StringComparison.OrdinalIgnoreCase))
                //if (submit.author.StartsWith("z", StringComparison.OrdinalIgnoreCase))
                //if (submit.full_link.Contains("dragon", StringComparison.OrdinalIgnoreCase))
                {


                    Console.WriteLine($"{submit.author}\t{DateTimeOffset.FromUnixTimeSeconds(submit.created_utc)}\t{submit.full_link}");
                }
            }


            //File.WriteAllLines("D:\\authors.dat", authors);
            //File.WriteAllLines("D:\\fullNames.dat", fullNames);
        }

        private async Task FetchNetwork()
        {
            List<RawSubmit> submits = new();


            while (true)
            {
                long? before = submits.Count == 0 ? null : submits[^1].created_utc;

                if (before.HasValue && before <= 1654812000)
                {
                    break;
                }

                var newRecords = await _rawData.GetSubmitRoot("plantclinic", null, before);

                Console.WriteLine(newRecords.data[0].full_link);

                submits.AddRange(newRecords.data);

                Console.WriteLine($"Fetched: {submits.Count}, Last: {DateTimeOffset.FromUnixTimeSeconds(submits.Last().created_utc)}");
            }

            var data = JsonSerializer.Serialize(submits);
            "D:\\sokadat.adat".WriteFile(data);
        }
    }


    public static class StringExtensions
    {
        public static void WriteFile(this string content, string path)
        {
            var fileInfo = new FileInfo(path);
            var dir = fileInfo.Directory;
            if (!dir.Exists)
            {
                dir.Create();
            }
            File.WriteAllText(path, content);
        }
    }
}

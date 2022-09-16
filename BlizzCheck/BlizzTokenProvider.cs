using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlizzCheck.Models;

namespace BlizzCheck
{
    public static class BlizzTokenProvider
    {
        public const string ClientId = "CNyRzFvW0wzsvTXq3GxACQ";
        public const string ClientSecret = "o1yc-vgfeY7dzydiEko-re7IxnURFQ";
        public const string TokenUrl = "https://www.reddit.com/api/v1/access_token";
        public static string Token = null;

        public static async Task<string> GetToken()
        {
            if (Token == null)
            {
                using var httpClient = new HttpClient();

                var authorizationString = $"{ClientId}:{ClientSecret}";
                var authorizationBytes = Encoding.UTF8.GetBytes(authorizationString);
                var authorizationBase64 = Convert.ToBase64String(authorizationBytes);

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authorizationBase64);
                httpClient.DefaultRequestHeaders.Add("User-Agent", Program.UserAgent);


                var result = await httpClient.PostAsync(TokenUrl, new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", "Dandiee88"),
                    new KeyValuePair<string, string>("password", "POIpoi234")
                }));
                var response = await result.Content.ReadAsStringAsync();

                var token = JsonSerializer.Deserialize<BlizzardToken>(response, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = SnakeCaseNamingPolicy.Default
                });


                Token = token.AccessToken;
            }

            return Token;
        }
    }
}

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using BlizzCheck.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Refit;

namespace BlizzCheck
{
    class Program
    {
        public static readonly string UserAgent = Uri.EscapeDataString("test script by /u/Dandiee88");
        public const string BlizzApiBaseUrl = "https://www.reddit.com";

        static async Task Main(string[] args)
        {

            var q = CreateHostBuilder(args).Build();
            var app = q.Services.GetRequiredService<App2>();

            await app.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                })
                .ConfigureServices((context, services) =>
                {
                    var deserOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web)
                    {
                        PropertyNamingPolicy = SnakeCaseNamingPolicy.Default
                    };
                    deserOptions.Converters.Add(new ReplyConverter());

                    var refitSettings =
                        new RefitSettings(
                            new SystemTextJsonContentSerializer(deserOptions));

                    services.AddTransient<DecorateAccessTokenHandler>();
                    
                    services.AddRefitClient<IBlizzClient>(_ => refitSettings)
                        .ConfigureHttpClient((_, client) =>
                        {
                            client.BaseAddress = new Uri("https://oauth.reddit.com");
                        }).AddHttpMessageHandler<DecorateAccessTokenHandler>();

                    services.AddRefitClient<IRawData>(_ => refitSettings)
                        .ConfigureHttpClient((_, client) =>
                        {
                            client.BaseAddress = new Uri("https://api.pushshift.io");
                        });

                    services.AddSingleton<App2>();
                    services.AddSingleton<CacheService>();
                });
    }

    public class DecorateAccessTokenHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await BlizzTokenProvider.GetToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
            request.Headers.Add("User-Agent", Program.UserAgent);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}

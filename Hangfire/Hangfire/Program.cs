using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.Logging;

namespace HangfireExample
{
    public class Program
    {
        // global execution count
        public static int ExecutionCount = 0;
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
             .ConfigureLogging(logging =>
             {
                 logging.ClearProviders();
                 logging.AddConsole();
                 logging.AddFilter(new Func<LogLevel, bool>(level => level == LogLevel.None));
             })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHangfire(configuration => configuration
                    .UseMemoryStorage());

                services.AddHangfireServer();

            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.Configure(app =>
                {
                    app.UseHangfireDashboard();
                    RecurringJob.AddOrUpdate("fetch-crypto-prices", () => FetchAndDisplayCryptoPrices(), "*/5 * * * * *");
                });
            });

        [AutomaticRetry(Attempts = 3, LogEvents = true, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        public static async Task FetchAndDisplayCryptoPrices()
        {
            try
            {
                //  throw an exception on the 3rd execution

                if (ExecutionCount == 2)
                {
                    throw new Exception("Something went wrong.");
                }

                ExecutionCount++;
                 
                


                Console.WriteLine("Fetching crypto prices...");

                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync("https://api.coingecko.com/api/v3/simple/price?ids=bitcoin,litecoin,ethereum,ripple,cardano&vs_currencies=zar");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                JObject jsonResult = JObject.Parse(responseBody);

                Console.WriteLine($"Bitcoin: R {string.Format("{0:n0}", jsonResult["bitcoin"]["zar"])}");
                Console.WriteLine($"Litecoin: R {string.Format("{0:n0}", jsonResult["litecoin"]["zar"])}");
                Console.WriteLine($"Ethereum: R {string.Format("{0:n0}", jsonResult["ethereum"]["zar"])}");
                Console.WriteLine($"Ripple: R {string.Format("{0:n0}", jsonResult["ripple"]["zar"])}");
                Console.WriteLine($"Cardano: R {string.Format("{0:n0}", jsonResult["cardano"]["zar"])}");

                Console.WriteLine("Job completed successfully.");
            }
            catch (Exception ex)
            {
                // Add custom error handling here
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }
}

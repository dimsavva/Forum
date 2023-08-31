using Newtonsoft.Json.Linq;
using Quartz;
using Quartz.Impl;

namespace UsingTPL
{
    public class CryptoJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            List<string> cryptos = new List<string> { "bitcoin", "ethereum", "litecoin", "ripple", "cardano" };
            List<string> failedCryptos = new List<string>();

            HttpClient httpClient = new HttpClient();
            await FetchCryptos(cryptos, httpClient, failedCryptos);

            if (failedCryptos.Count > 0)
            {
                Task.Delay(1000).Wait();
                Console.WriteLine("Retrying failed cryptos...");
                await FetchCryptos(failedCryptos, httpClient, new List<string>());
            }
            else
            {
                Console.WriteLine("No failed cryptos to retry.");
                Console.WriteLine("Job complete.");

            }

        }

        private async Task FetchCryptos(List<string> cryptos, HttpClient httpClient, List<string> failedCryptos)
        {
            await Task.Run(() =>
            {
                Parallel.ForEach(cryptos, async (crypto) =>
                {
                    try
                    {
                        Random random = new Random();
                        if (random.Next(0, 10) < 3)  // Roughly 30% chance of failure
                        {
                            throw new Exception("Simulated failure.");
                        }

                        HttpResponseMessage response = await httpClient.GetAsync($"https://api.coingecko.com/api/v3/simple/price?ids={crypto}&vs_currencies=zar");
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        JObject jsonResult = JObject.Parse(responseBody);
                        Console.WriteLine($"{crypto.ToUpper()}: R {String.Format("{0:n0}", jsonResult[crypto]["zar"])}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred while fetching {crypto}: {ex.Message}");
                        failedCryptos.Add(crypto);
                    }
                });
            });
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            IScheduler scheduler = await schedulerFactory.GetScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<CryptoJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("cryptoTrigger")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(30)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}

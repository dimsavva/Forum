
using Newtonsoft.Json.Linq;
using Quartz;
using Quartz.Impl;

namespace QuartzNetExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Initialize Quartz.NET scheduler
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();

            // Start scheduler
            await scheduler.Start();

            // Add custom error listener
            scheduler.ListenerManager.AddJobListener(new ErrorListener());

            // Define a job and associate it with FetchAndDisplayCryptoPrices method
            IJobDetail job = JobBuilder.Create<FetchAndDisplayCryptoPrices>()
                .WithIdentity("CryptoPriceJob", "CryptoPriceGroup")
                .Build();

            //// Create a trigger for the job
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("CryptoPriceTrigger", "CryptoPriceGroup")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();

            // Schedule the job
            await scheduler.ScheduleJob(job, trigger);

            // Prevent the app from exiting immediately
            Console.WriteLine("Press any key to close the application.");
            Console.ReadKey();
        }
    }

    public class FetchAndDisplayCryptoPrices : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                Console.WriteLine("Fetching crypto prices...");

                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync("https://api.coingecko.com/api/v3/simple/price?ids=bitcoin,litecoin,ethereum,ripple,cardano&vs_currencies=zar");

                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                JObject jsonResult = JObject.Parse(responseBody);

                Console.WriteLine($"Bitcoin: R {String.Format("{0:n0}", jsonResult["bitcoin"]["zar"])}");
                Console.WriteLine($"Litecoin: R {String.Format("{0:n0}", jsonResult["litecoin"]["zar"])}");
                Console.WriteLine($"Ethereum: R {String.Format("{0:n0}", jsonResult["ethereum"]["zar"])}");
                Console.WriteLine($"Ripple: R {String.Format("{0:n0}", jsonResult["ripple"]["zar"])}");
                Console.WriteLine($"Cardano: R {String.Format("{0:n0}", jsonResult["cardano"]["zar"])}");

                Console.WriteLine("Job completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }




    public class SimpleJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Executing SimpleJob.");
            return Task.CompletedTask;
        }
    }


    public class ErrorListener : IJobListener
    {
        public string Name => "ErrorListener";

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }


        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? jobException, CancellationToken cancellationToken = default)
        {
            if (jobException != null)
            {
                Console.WriteLine($"Job failed with exception: {jobException.Message}");
            }

            return Task.CompletedTask;
        }
    }
}

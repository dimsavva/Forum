using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HostedServiceExampleConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<ConsoleHostedService>();
                });
    }

    public class ConsoleHostedService : IHostedService
    {
        private readonly ILogger _logger;

        public ConsoleHostedService(ILogger<ConsoleHostedService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Hosted service starting");

            return Task.Factory.StartNew(async () =>
            {
                // Run background task until a cancellation is requested
                while (!cancellationToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Hosted service executing - {0}", DateTime.Now);
                    try
                    {
                        await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
                    }
                    catch (OperationCanceledException) { }
                }
            }, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Hosted service stopping");
            return Task.CompletedTask;
        }
    }
}

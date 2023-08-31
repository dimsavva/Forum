namespace BackgroundServiceExampleWeb.BackgroundServices
{
    public class MyHostedService : IHostedService
    {
        private readonly ILogger<MyHostedService> _logger;

        public MyHostedService(ILogger<MyHostedService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Hosted service starting");
            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Hosted service executing - {0}", DateTime.Now);
                    await Task.Delay(new TimeSpan(0, 0, 1), cancellationToken);
                }
            }, cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Hosted service stopping");
            return Task.CompletedTask;
        }
    }
}

namespace BackgroundServiceExampleWeb.BackgroundServices
{
    public class MyBackgroundService : BackgroundService
    {
        private readonly ILogger<MyBackgroundService> _logger;
        public MyBackgroundService(ILogger<MyBackgroundService> logger)
        {
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogWarning("Background service starting");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogWarning("Background service executing - {0}", DateTime.Now);
                await Task.Delay(new TimeSpan(0, 0, 1), stoppingToken);
            }
        }
    }
}

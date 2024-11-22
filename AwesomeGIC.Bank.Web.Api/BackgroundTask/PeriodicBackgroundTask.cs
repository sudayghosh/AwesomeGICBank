
namespace AwesomeGIC.Bank.Web.Api.BackgroundTask
{
    public class PeriodicBackgroundTask : BackgroundService
    {
        private readonly TimeSpan timeSpan = TimeSpan.FromSeconds(10);
        private readonly ILogger<PeriodicBackgroundTask> _logger;

        public PeriodicBackgroundTask(ILogger<PeriodicBackgroundTask> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer periodicTimer = new PeriodicTimer(timeSpan);
            while (!stoppingToken.IsCancellationRequested
                && await periodicTimer.WaitForNextTickAsync(stoppingToken))
            {
                _logger.LogInformation("Executing...");
            }
        }
    }
}

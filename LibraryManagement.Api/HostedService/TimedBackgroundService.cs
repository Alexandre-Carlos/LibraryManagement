using LibraryManagement.Application.Commands.Loans.Notify;
using LibraryManagement.Application.Configuration;
using System.Text;

public class TimedBackgroundService : BackgroundService
{
    private readonly ILogger<TimedBackgroundService> _logger;
    private string _workerStartTime;

    public IServiceProvider Services { get; }

    public TimedBackgroundService(IServiceProvider services, ILogger<TimedBackgroundService> logger, ApplicationConfig appConfig)
    {
        Services = services;
        _logger = logger;
        _workerStartTime = appConfig.WorkerConfig.WorkerStartTime;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var secondsToWaitFirstTime = CalculateFirstTimeStart(_workerStartTime);
        _logger.LogInformation("Timed Background Service running.");

        await Task.Delay(TimeSpan.FromSeconds(secondsToWaitFirstTime), stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Timed Background Service is working.");

            await DoWork(stoppingToken);
           
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }

        _logger.LogInformation("Timed Background Service is stopping.");
    }

    private async Task DoWork(CancellationToken stoppingToken)
    {
        _logger.LogInformation(
            "Consume Scoped Service Hosted Service is working.");

        using (var scope = Services.CreateScope())
        {
            var scopedProcessingService = 
                scope.ServiceProvider
                    .GetRequiredService<INotifyDelayService>();

             await scopedProcessingService.Execute(stoppingToken);
        }
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation(
            "Consume Scoped Service Hosted Service is stopping.");

        await base.StopAsync(stoppingToken);
    }

    private int CalculateFirstTimeStart(string firstExecution)
    {
        var splitTime = firstExecution.Split(':');

        var now = DateTime.Now;
        var hours = Convert.ToInt32(splitTime[0]) - now.Hour;
        var minutes = Convert.ToInt32(splitTime[1]) - now.Minute;
        var seconds = Convert.ToInt32(splitTime[2]) - now.Second;
        var secondsTillStart = hours * 3600 + minutes * 60 + seconds;
        return secondsTillStart;
    }
}
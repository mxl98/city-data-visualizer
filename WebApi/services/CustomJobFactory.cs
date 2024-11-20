using Quartz;
using Quartz.Spi;

/// <summary>
/// Represents a factory for custom job instances creation.
/// </summary>
public class CustomJobFactory : IJobFactory
{
    private readonly IServiceProvider _serviceProvider;

    public CustomJobFactory( IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        return _serviceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
    }

    public void ReturnJob(IJob job) {}
}
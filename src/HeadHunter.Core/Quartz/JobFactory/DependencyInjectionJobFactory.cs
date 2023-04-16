using Quartz;
using Quartz.Spi;

namespace HeadHunter.Core.Quartz.JobFactory;

public class DependencyInjectionJobFactory : IJobFactory
{
    private readonly IServiceProvider _serviceProvider;

    public DependencyInjectionJobFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        try
        {
            return (IJob)_serviceProvider.GetService(bundle.JobDetail.JobType);
        }
        catch (Exception e)
        {
            throw new SchedulerException($"Problem while instantiating job {bundle.JobDetail.Key}", e);
        }
    }

    public void ReturnJob(IJob job)
    {
        var disposable = job as IDisposable;  
        disposable?.Dispose();
    }
}
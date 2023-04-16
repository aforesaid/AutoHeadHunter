using HeadHunter.Core.Quartz.JobFactory;
using HeadHunter.Core.Quartz.Jobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace HeadHunter.Core.Quartz;

public static class QuartzConfiguration
{
    public static IServiceCollection AddQuartzConfiguration(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        const string quartzConfiguration = "Quartz";
        const int threadCount = 10000;

        serviceCollection.Configure<QuartzOptions>(configuration.GetSection(quartzConfiguration));
        serviceCollection.Configure<QuartzOptions>(options =>
        {
            options.Scheduling.IgnoreDuplicates = true;
            options.Scheduling.OverWriteExistingData = true;
        });
        
        AddJobs(serviceCollection);

        serviceCollection.AddQuartz(q =>
        {
            q.UseJobFactory<DependencyInjectionJobFactory>();

            q.SchedulerId = quartzConfiguration;

            q.UseSimpleTypeLoader();
            q.UseInMemoryStore();
            q.UseMicrosoftDependencyInjectionJobFactory();

            q.UseDefaultThreadPool(tp =>
            {
                tp.MaxConcurrency = threadCount;
            });
            
            q.ScheduleJob<HeadHunterTouchResumeJob>(trigger => trigger
                .WithIdentity(nameof(HeadHunterTouchResumeJob))
                .StartNow()
                .WithDailyTimeIntervalSchedule(x => x.WithIntervalInHours(4)
                    .InTimeZone(TimeZoneInfo.Utc))
            );
            
            q.ScheduleJob<HeadHunterRespondToSupportedVacanciesJob>(trigger => trigger
                .WithIdentity(nameof(HeadHunterRespondToSupportedVacanciesJob))
                .StartNow()
                .WithDailyTimeIntervalSchedule(x => x.WithIntervalInHours(12)
                    .InTimeZone(TimeZoneInfo.Utc))
            );
        });
        
        serviceCollection.AddQuartzHostedService(options =>
        {
            options.AwaitApplicationStarted = true;
            options.WaitForJobsToComplete = true;
        });

        return serviceCollection;
    }
    
    private static void AddJobs(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<HeadHunterTouchResumeJob>();
        serviceCollection.AddScoped<HeadHunterRespondToSupportedVacanciesJob>();
    }
}
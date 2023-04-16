using System.Reflection;
using HeadHunter.Core.Clients.AntiCaptcha;
using HeadHunter.Core.Clients.HeadHunter;
using HeadHunter.Core.Configuration;
using HeadHunter.Core.Quartz;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HeadHunter.Core;

public static class HeadHunterConfiguration
{
    public static IServiceCollection AddCoreServices(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddQuartzConfiguration(configuration);
        
        serviceCollection.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        serviceCollection.AddScoped<IHeadHunterClient, HeadHunterClient>();
        serviceCollection.AddScoped<IAntiCaptchaApiClient, AntiCaptchaApiClient>();
        
        serviceCollection.Configure<HeadHunterSettings>(configuration.GetSection(nameof(HeadHunterSettings)));
        
        return serviceCollection;
    }
}
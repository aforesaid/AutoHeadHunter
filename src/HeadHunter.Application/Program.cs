using HeadHunter.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Serilog;

const string settings = "appsettings.json";

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

var host = new WebHostBuilder()
    .UseKestrel()
    .ConfigureLogging((host, loggingBuilder) =>
    {  
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(host.Configuration)
            .Destructure.AsScalar<JObject>()
            .Destructure.AsScalar<JArray>()
            .Enrich.FromLogContext()
            .CreateLogger();
        
        loggingBuilder.AddSerilog(logger, dispose: true);
    })
    .ConfigureAppConfiguration(x =>
        x.AddJsonFile(settings, optional: true)
            .AddEnvironmentVariables())
    .ConfigureServices((host, serviceCollection) =>
    {
        serviceCollection.AddCoreServices(host.Configuration);
    })
    .Configure(_ =>
    { })
    .Build();

await host.RunAsync();
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using TestingAzureFunctions.Fnt;
using TestingAzureFunctions.Fnt.Common.Services;
using TestingAzureFunctions.Models;
using TestingAzureFunctions.Services;
using TestingAzureFunctions.Services.Abstract;

[assembly: FunctionsStartup(typeof(Startup))]
namespace TestingAzureFunctions.Fnt
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<IConfigurationService, ConfigurationService>();
            builder.Services.TryAdd(ServiceDescriptor.Singleton<ILoggerFactory, LoggerFactory>());
            builder.Services.TryAdd(ServiceDescriptor.Singleton(typeof(ILogger<>), typeof(Logger<>)));
            builder.Services.AddLogging();

            var serviceProvider = builder.Services.BuildServiceProvider();
            var configurationtionService = serviceProvider.GetService<IConfigurationService>();
            var configuration = configurationtionService.GetConfiguration();
            var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();

            builder.Services.AddSingleton<IAppSettings>(appSettings);
            builder.Services.AddSingleton<IBlobStorageService, BlobStorageService>();
        }
    }
}

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using TestingAzureFunctions.Fnt;
using TestingAzureFunctions.Services;
using TestingAzureFunctions.Services.Abstract;

[assembly: FunctionsStartup(typeof(Startup))]
namespace TestingAzureFunctions.Fnt
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.TryAdd(ServiceDescriptor.Singleton<ILoggerFactory, LoggerFactory>());
            builder.Services.TryAdd(ServiceDescriptor.Singleton(typeof(ILogger<>), typeof(Logger<>)));
            builder.Services.AddLogging();
            builder.Services.AddSingleton<IBlobStorageService, BlobStorageService>();
        }
    }
}

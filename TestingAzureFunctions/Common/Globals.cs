using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TestingAzureFunctions.Fnt.Common
{
    public static class Globals
    {
        private static IConfigurationBuilder builder;

        public static IConfiguration GetConfiguration(ExecutionContext executionContext)
        {
            return GetConfiguration(executionContext.FunctionAppDirectory);
        }

        public static IConfiguration GetConfiguration()
        {
            return GetConfiguration(Directory.GetCurrentDirectory());
        }

        private static IConfiguration GetConfiguration(string basePath)
        {
            if (builder == null)
            {
                builder = new ConfigurationBuilder()
                 .SetBasePath(basePath)
                 .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                 .AddEnvironmentVariables();
            }
            return builder.Build();
        }
    }
}

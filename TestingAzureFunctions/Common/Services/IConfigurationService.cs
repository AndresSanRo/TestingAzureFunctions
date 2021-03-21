using Microsoft.Extensions.Configuration;

namespace TestingAzureFunctions.Fnt.Common.Services
{    
    public interface IConfigurationService
    {
        IConfiguration GetConfiguration();
    }

    public class ConfigurationService : IConfigurationService
    {
        public IConfiguration GetConfiguration() => Globals.GetConfiguration();
    }
}

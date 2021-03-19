using Autofac;
using TestingAzureFunctions.Services.Abstract;

namespace TestingAzureFunctions.Services
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BlobStorageService>().As<IBlobStorageService>();
        }
    }
}

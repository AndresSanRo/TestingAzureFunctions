namespace TestingAzureFunctions.Models
{
    public class AppSettings : IAppSettings
    {
        public BlobStorageSettings BlobStorageSettings { get; set; }
    }
}

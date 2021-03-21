namespace TestingAzureFunctions.Models
{
    public interface IAppSettings
    {
        BlobStorageSettings BlobStorageSettings { get; set; }
    }
}

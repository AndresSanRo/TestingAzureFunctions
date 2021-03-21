namespace TestingAzureFunctions.Models
{
    public class BlobStorageSettings
    {
        public string StorageConnectionString { get; set; }

        public string ContainerName { get; set; }

        public string BlobName { get; set; }
    }
}

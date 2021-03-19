using TestingAzureFunctions.Services.Abstract;
using TestingAzureFunctions.Models;
using Azure;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using System;

namespace TestingAzureFunctions.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        public string ConnectionString { get; private set; }
        public string ContainerName { get; private set; }

        public BlobStorageService(IAppSettings appSettings)
        {
            ConnectionString = appSettings.BlobStorageSettings.StorageConnectionString;
            ContainerName = appSettings.BlobStorageSettings.ContainerName;
        }

        public AsyncPageable<BlobItem> GetBlobsListAsync(BlobContainerClient container)
        {
            try
            {
                return container.GetBlobsAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Container: {container.Name} - Error while listing blobs.", ex);
            }
        }

        public BlobContainerClient GetBlobContainerClient() => new BlobContainerClient(ConnectionString, ContainerName);
    }
}

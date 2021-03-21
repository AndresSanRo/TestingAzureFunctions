using TestingAzureFunctions.Services.Abstract;
using TestingAzureFunctions.Models;
using Azure;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestingAzureFunctions.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        public string ConnectionString { get; private set; }
        
        public string ContainerName { get; private set; }

        public string BlobName { get; set; }

        public BlobStorageService(IAppSettings appSettings)
        {
            ConnectionString = appSettings.BlobStorageSettings.StorageConnectionString;
            ContainerName = appSettings.BlobStorageSettings.ContainerName;
            BlobName = appSettings.BlobStorageSettings.BlobName;
        }

        public async Task<List<string>> GetBlobsListAsync(BlobContainerClient container)
        {
            try
            {
                List<string> blobNames = new List<string>();
                await foreach (BlobItem blobItem in container.GetBlobsAsync())
                {
                    blobNames.Add(blobItem.Name);
                }
                return blobNames;
            }
            catch (Exception ex)
            {
                throw new Exception($"Container: {container.Name} - Error while listing blobs.", ex);
            }
        }

        public BlobContainerClient GetBlobContainerClient() => new BlobContainerClient(ConnectionString, ContainerName);

        public async Task<BlobDownloadInfo> DownloadBlobAsync()
        {
            var client = new BlobClient(ConnectionString, ContainerName, BlobName);
            return await client.DownloadAsync();
        }
    }
}

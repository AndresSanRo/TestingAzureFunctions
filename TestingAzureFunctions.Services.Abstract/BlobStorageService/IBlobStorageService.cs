using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace TestingAzureFunctions.Services.Abstract
{
    public interface IBlobStorageService
    {
        AsyncPageable<BlobItem> GetBlobsListAsync(BlobContainerClient container);

        BlobContainerClient GetBlobContainerClient();
    }
}

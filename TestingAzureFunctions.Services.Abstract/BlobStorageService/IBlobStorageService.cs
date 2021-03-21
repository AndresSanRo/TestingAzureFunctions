using Azure.Storage.Blobs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestingAzureFunctions.Services.Abstract
{
    public interface IBlobStorageService
    {
        Task<List<string>> GetBlobsListAsync(BlobContainerClient container);

        BlobContainerClient GetBlobContainerClient();

        BlobClient GetBlobClient();
    }
}

using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestingAzureFunctions.Services.Abstract
{
    public interface IBlobStorageService
    {
        Task<List<string>> GetBlobsListAsync(BlobContainerClient container);

        BlobContainerClient GetBlobContainerClient();

        Task<BlobDownloadInfo> DownloadBlobAsync();
    }
}

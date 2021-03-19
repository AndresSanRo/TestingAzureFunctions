using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TestingAzureFunctions.Services.Abstract;

namespace TestingAzureFunctions.Fnt.Functions
{
    public class GetBlobList
    {
        private readonly IBlobStorageService BlobStoregeService;
        public GetBlobList(IBlobStorageService BlobStoregeService)
        {
            this.BlobStoregeService = BlobStoregeService;
        }

        [FunctionName("GetBlobList")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var client = BlobStoregeService.GetBlobContainerClient();
            await foreach (BlobItem blobItem in BlobStoregeService.GetBlobsListAsync(client))
            {
                log.LogInformation(blobItem.Name);
            }

            return new OkObjectResult("Ok");
        }
    }
}

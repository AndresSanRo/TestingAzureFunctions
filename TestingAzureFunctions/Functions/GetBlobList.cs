using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
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

        [FunctionName(nameof(GetBlobList))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string proccesId = Guid.NewGuid().ToString();
            try
            {
                log.LogInformation($"{nameof(GetBlobList)} proccess with guid: {proccesId} started at: {DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss tt")}");
                var client = BlobStoregeService.GetBlobContainerClient();
                List<string> blobNames = new List<string>();
                await foreach (BlobItem blobItem in BlobStoregeService.GetBlobsListAsync(client))
                {
                    blobNames.Add(blobItem.Name);                    
                }

                return new OkObjectResult(blobNames.ToArray());
            }
            catch(Exception ex)
            {
                log.LogError($"{nameof(GetBlobList)} proccess with guid: {proccesId} - Error getting the blob list, exception message: {ex.Message}");
                return new ExceptionResult(ex, false);
            }            
        }
    }
}

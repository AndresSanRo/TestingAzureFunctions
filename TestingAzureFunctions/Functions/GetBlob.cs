using Azure.Storage.Blobs.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using TestingAzureFunctions.Services.Abstract;

namespace TestingAzureFunctions.Fnt.Functions
{
    public class GetBlob
    {
        private readonly IBlobStorageService BlobStorageService;

        public GetBlob(IBlobStorageService BlobStorageService)
        {
            this.BlobStorageService = BlobStorageService;
        }

        [FunctionName(nameof(GetBlob))]
        public async Task Run([TimerTrigger("0 0/2 * * * *")]TimerInfo myTimer, ILogger log)
        {
            string proccesId = Guid.NewGuid().ToString();
            try
            {
                log.LogInformation($"{nameof(GetBlob)} proccess with guid: {proccesId} started at: {DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss tt")}");
                
                log.LogInformation($"{nameof(GetBlob)} proccess with guid: {proccesId} - Get blob client");
                var client = BlobStorageService.GetBlobClient();
                
                log.LogInformation($"{nameof(GetBlob)} proccess with guid: {proccesId} - Download blob");
                BlobDownloadInfo blob = await client.DownloadAsync();
                
                log.LogInformation($"{nameof(GetBlob)} proccess with guid: {proccesId} - Reading blob content");
                using (var streamReader = new StreamReader(blob.Content))
                {
                    while (!streamReader.EndOfStream)
                    {
                        string line = await streamReader.ReadLineAsync();
                        log.LogInformation($"{nameof(GetBlob)} proccess with guid: {proccesId} - Blob content: {line}");
                    }
                }
                log.LogInformation($"{nameof(GetBlob)} proccess with guid: {proccesId} - blob content readed successfully");
            }
            catch(Exception ex)
            {
                log.LogError($"{nameof(GetBlob)} proccess with guid: {proccesId} - Error getting the blob, exception message: {ex.Message}");
            }
        }
    }
}

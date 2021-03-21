using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using TestingAzureFunctions.Fnt.Functions;
using TestingAzureFunctions.Services.Abstract;
using Xunit;

namespace TestingAzureFunctions.Tests
{
    public class GetBlobsListTests
    {
        [Fact]
        public async Task GetBlobsList_withData_returnArrayOfBlobNames()
        {
            var listLogger = (ListLogger)TestFactory.CreateLogger(LoggerTypes.List);
            var req = TestFactory.CreateHttpRequest("mockRequest", "mockRequest");
            var mockBlobService = GetMockBlobService(true);
            var fnt = new GetBlobsList(mockBlobService);

            var response = (OkObjectResult)await fnt.Run(req, listLogger);

            string[] blobNames = response.Value as string[];
            Assert.NotNull(response);
            Assert.Equal(200, response.StatusCode);
            Assert.NotEmpty(blobNames);
            Assert.Equal(5, blobNames.Length);
            Assert.Contains("Blob list retieved with 5 blob names", listLogger.Logs[2]);
        }

        [Fact]
        public async Task GetBlobsList_withoutData_returnEmptyArray()
        {
            var listLogger = (ListLogger)TestFactory.CreateLogger(LoggerTypes.List);
            var req = TestFactory.CreateHttpRequest("mockRequest", "mockRequest");
            var mockBlobService = GetMockBlobService(false);
            var fnt = new GetBlobsList(mockBlobService);

            var response = (OkObjectResult)await fnt.Run(req, listLogger);

            string[] blobNames = response.Value as string[];
            Assert.NotNull(response);
            Assert.Equal(200, response.StatusCode);
            Assert.Empty(blobNames);
            Assert.Contains("There are no blobs in this container", listLogger.Logs[2]);
        }

        [Fact]
        public async Task GetBlobsList_raiseException_returnExceptionResult()
        {
            var listLogger = (ListLogger)TestFactory.CreateLogger(LoggerTypes.List);
            var req = TestFactory.CreateHttpRequest("mockRequest", "mockRequest");
            var mockBlobService = new Mock<IBlobStorageService>();
            mockBlobService.Setup(x => x.GetBlobsListAsync(It.IsAny<BlobContainerClient>())).Throws(new Exception());
            var fnt = new GetBlobsList(mockBlobService.Object);

            var response = await fnt.Run(req, listLogger);

            Assert.Contains("Error getting the blob list", listLogger.Logs.Last());
        }

        private IBlobStorageService GetMockBlobService(bool withData)
        {
            var mockBlobService = new Mock<IBlobStorageService>();
            mockBlobService.Setup(x => x.GetBlobsListAsync(It.IsAny<BlobContainerClient>())).Returns(Task.FromResult(GetMockBlobList(withData)));
            return mockBlobService.Object;
        }

        private List<string> GetMockBlobList(bool withData)
        {
            List<string> blobList = new List<string>();
            if (withData)
            {
                blobList.Add("doc1.txt");
                blobList.Add("doc2.txt");
                blobList.Add("doc3.txt");
                blobList.Add("doc4.txt");
                blobList.Add("doc5.txt");
            }
            return blobList;
        }
    }
}

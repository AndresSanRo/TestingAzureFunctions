using Azure.Storage.Blobs.Models;
using Moq;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingAzureFunctions.Fnt.Functions;
using TestingAzureFunctions.Services.Abstract;
using Xunit;

namespace TestingAzureFunctions.Tests
{
    public class GetBlobTests
    {

        [Fact]
        public async Task GetBlob_withData_logBlobContent()
        {
            var listLogger = (ListLogger)TestFactory.CreateLogger(LoggerTypes.List);
            var mockBlobService = new Mock<IBlobStorageService>();
            mockBlobService.Setup(x => x.DownloadBlobAsync()).Returns(Task.FromResult(GetMockBlob()));
            var fnt = new GetBlob(mockBlobService.Object);

            await fnt.Run(null, listLogger);

            Assert.Contains("Blob content: doc", listLogger.Logs[3]);
            Assert.Contains("blob content readed successfully", listLogger.Logs.Last());
        }

        [Fact]
        public async Task GetBlob_throwException_logError()
        {
            var listLogger = (ListLogger)TestFactory.CreateLogger(LoggerTypes.List);
            var mockBlobService = new Mock<IBlobStorageService>();
            mockBlobService.Setup(x => x.DownloadBlobAsync()).Throws(new Exception());
            var fnt = new GetBlob(mockBlobService.Object);

            await fnt.Run(null, listLogger);

            Assert.Contains("Error getting the blob", listLogger.Logs.Last());
        }

        #region Private methods        

        private BlobDownloadInfo GetMockBlob()
        {            
            byte[] firstString = Encoding.UTF8.GetBytes("doc");
            MemoryStream str =  new MemoryStream(firstString);
            
            BlobDownloadInfo blob = BlobsModelFactory.BlobDownloadInfo(
                        DateTime.UtcNow, 0, BlobType.Block, null, null, null, null, null, null,
                        CopyStatus.Pending, null, LeaseDurationType.Infinite, null, LeaseState.Available,
                        null, LeaseStatus.Locked, null, null, default, 0, null, false, null, null,
                        null, 0, null, null, null, str);
            return blob;
        }        

        #endregion
    }
}

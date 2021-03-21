using Xunit;

namespace TestingAzureFunctions.Tests
{
    public class GetBlobsListTests
    {
        [Fact]
        public void GetBlobsList_returnArrayOnBlobNames()
        {
            var listLogger = (ListLogger)TestFactory.CreateLogger(LoggerTypes.List);
        }
    }
}

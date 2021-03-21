using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace TestingAzureFunctions.Tests.ListLogger
{
    public class TestFactory
    {
        public static ILogger CreateLogger(LoggerTypes type = LoggerTypes.Null)
        {
            if (type == LoggerTypes.List)
                return new ListLogger();
            else
                return NullLoggerFactory.Instance.CreateLogger("Null Logger");
        }
    }
}

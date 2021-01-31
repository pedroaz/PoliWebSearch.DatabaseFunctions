using Microsoft.Extensions.Logging;
using Moq;
using PoliWebSearch.DatabaseFunctions.Services.Database;

namespace PoliWebSearch.DatabaseFunctions.Tests.Services.Database
{
    public class DatabaseServiceUnitTests
    {
        private DatabaseService databaseService;
        private readonly Mock<ILogger> loggerMock = new Mock<ILogger>();

        public DatabaseServiceUnitTests()
        {
            databaseService = new DatabaseService(loggerMock.Object);
        }
    }
}

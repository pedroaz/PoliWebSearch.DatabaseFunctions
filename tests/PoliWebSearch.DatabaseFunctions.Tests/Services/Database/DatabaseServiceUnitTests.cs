using PoliWebSearch.DatabaseFunctions.Services.Database;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using Moq;
using Microsoft.Extensions.Logging;

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

        [Fact]
        public void OnDatabaseInitialize_ShouldCreateGremlinServer()
        {
            // Arrange
            Environment.SetEnvironmentVariable("DatabaseName", "name");
            Environment.SetEnvironmentVariable("GraphName", "graph");
            Environment.SetEnvironmentVariable("HostName", "host");
            Environment.SetEnvironmentVariable("MasterKey", "master");
            // Act
            databaseService.Initialize();
            // Assert
            databaseService.GetDatabaseServiceStatus().Should().Be(DatabaseServiceStatus.Started);
        }

    }
}

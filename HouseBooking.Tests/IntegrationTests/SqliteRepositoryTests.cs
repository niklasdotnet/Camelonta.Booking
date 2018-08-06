using System.IO;
using NUnit.Framework;
using FluentAssertions;

namespace HouseBooking.Tests.IntegrationTests
{
    [TestFixture]
    class SqliteRepositoryTests
    {
        private string ConnectionString { get; set; }

        [OneTimeSetUp]
        public void Setup()
        {
            ConnectionString = $"URI=file:{TestContext.CurrentContext.TestDirectory}\\Database\\HouseBooking.Data.sqlite";
        }

        [Test]
        public void DatabaseShouldExist()
        {
            //Arrange
            var dbPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Database", "HouseBooking.Data.sqlite");

            //Act
            var exists = File.Exists(dbPath);

            //Assert
            exists.Should().BeTrue();
        }
    }
}

using System.IO;
using NUnit.Framework;
using FluentAssertions;

namespace Camelonta.Tests.IntegrationTests
{
    [TestFixture]
    class SqliteRepositoryTests
    {
        private string _connectionString { get; set; }

        [OneTimeSetUp]
        public void Setup()
        {
            _connectionString = $"URI=file:{TestContext.CurrentContext.TestDirectory}\\Database\\Camelonta.Data.sqlite";
        }

        [Test]
        public void DatabaseShouldExist()
        {
            //Arrange
            var dbPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Database", "Camelonta.Data.sqlite");

            //Act
            var exists = System.IO.File.Exists(dbPath);

            //Assert
            exists.Should().BeTrue();
        }
    }
}

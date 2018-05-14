using System;
using System.IO;
using System.Linq;
using Camelonta.Backend.Infrastructure;
using NUnit.Framework;
using Camelonta.Backend.Repository.SQLite;
using FluentAssertions;

namespace Camelonta.Tests.IntegrationTests
{
    [TestFixture]
    public class SqliteHousingRepositoryTests
    {
        private string _connectionString { get; set; }

        [OneTimeSetUp]
        public void Setup()
        {
            _connectionString = $"URI=file:{TestContext.CurrentContext.TestDirectory}\\Database\\Camelonta.Data.sqlite";           
        }

        [Test]
        public void ShouldGetAtLeastOneHousing()
        {
            //Arrange
            var repo = new SqliteHousingRepository(_connectionString);

            //Act
            var housings = repo.GetHousings();

            //Assert
            housings.Should().NotBeNull();
            housings.Count().Should().BeGreaterOrEqualTo(1);
        }

        [Test]
        public void ShouldGetHousingById()
        {
            //Arrange
            var repo = new SqliteHousingRepository(_connectionString);

            //Act
            var housing = repo.GetHousingById(1);

            //Assert
            housing.Should().NotBeNull();
        }
    }
}

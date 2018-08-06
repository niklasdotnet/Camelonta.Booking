using System.Linq;
using NUnit.Framework;
using HouseBooking.Backend.Repository.SQLite;
using FluentAssertions;

namespace HouseBooking.Tests.IntegrationTests
{
    [TestFixture]
    public class SqliteHousingRepositoryTests
    {
        private string ConnectionString { get; set; }

        [OneTimeSetUp]
        public void Setup()
        {
            ConnectionString = $"URI=file:{TestContext.CurrentContext.TestDirectory}\\Database\\HouseBooking.Data.sqlite";           
        }

        [Test]
        public void ShouldGetAtLeastOneHousing()
        {
            //Arrange
            var repo = new SqliteHousingRepository(ConnectionString);

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
            var repo = new SqliteHousingRepository(ConnectionString);

            //Act
            var housing = repo.GetHousingById(1);

            //Assert
            housing.Should().NotBeNull();
        }
    }
}

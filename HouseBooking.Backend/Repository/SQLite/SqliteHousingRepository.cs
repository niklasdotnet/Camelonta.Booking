using System.Collections.Generic;
using System.Data.SQLite;
using HouseBooking.Backend.Models;

namespace HouseBooking.Backend.Repository.SQLite
{
    public class SqliteHousingRepository : IHousingRepository
    {
        private readonly string _connectionString;

        public SqliteHousingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Housing> GetHousings()
        {
            var housings = new List<Housing>();
            const string sqlQuery = "SELECT [Id], [Name], [BaseDayFee], [DayPriceFactor], [Image] FROM [Housing]";
            using (var connection = new SQLiteConnection(_connectionString))
            using (var command = new SQLiteCommand(sqlQuery, connection))
            {
                connection.Open();
                SQLiteDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    var housing = new Housing
                    {
                        Id = (long)reader["Id"],
                        Name = reader["Name"].ToString(),
                        BaseDayFee = (long)reader["BaseDayFee"],
                        DayPriceFactor = (decimal)reader["DayPriceFactor"],
                        Image = reader["Image"].ToString()
                    };

                    housings.Add(housing);
                }

                reader.Close();
                reader.Dispose();
            }

            return housings;
        }

        public Housing GetHousingById(long id)
        {
            var sqlQuery = $"SELECT [Id], [Name], [BaseDayFee], [DayPriceFactor], [Image] FROM [Housing] WHERE Id = '{id}'";
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                SQLiteDataReader reader;
                using (var command = new SQLiteCommand(sqlQuery, connection))
                {
                    reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                }

                if (!reader.Read()) return null;

                var housing = new Housing()
                {
                    Id = (long)reader["Id"],
                    Name = reader["Name"].ToString(),
                    BaseDayFee = (long)reader["BaseDayFee"],
                    DayPriceFactor = (decimal)reader["DayPriceFactor"],
                    Image = reader["Image"].ToString()
                };

                reader.Close();
                reader.Dispose();

                return housing;
            }
        }
    }
}
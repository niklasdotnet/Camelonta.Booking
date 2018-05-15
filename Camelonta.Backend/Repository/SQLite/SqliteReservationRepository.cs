using System;
using System.Data.SQLite;
using Camelonta.Backend.Models;

namespace Camelonta.Backend.Repository.SQLite
{
    public class SqliteReservationRepository : IReservationRepository
    {
        private readonly string _connectionString;

        public SqliteReservationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int CreateReservation(Reservation model)
        {
            // This logic should exist in a service.
            // MVC Controller receives an IService.
            // An IRepository is then injected into that IService.

            var housingRepo = new SqliteHousingRepository(_connectionString);
            var housing = housingRepo.GetHousingById(model.HousingId);

            var numberOfDays = model.DateTo.Subtract(model.DateFrom).Days;
            var price = housing.BaseDayFee * numberOfDays * housing.DayPriceFactor;

            model.ReservationNumber = Guid.NewGuid().ToString().ToUpper().Substring(0, 5);
            
            var reservationInsertQuery = "INSERT INTO [Reservation] ([HousingId], [ReservationNumber], [PersonalNumber], [DateFrom], [DateTo], [Price]) VALUES(@HousingId, @ReservationNumber, @PersonalNumber, @DateFrom, @DateTo, @Price);";

            using (var connection = new SQLiteConnection(_connectionString))
            using (var command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = reservationInsertQuery;

                command.Parameters.AddWithValue("HousingId", model.HousingId);
                command.Parameters.AddWithValue("ReservationNumber", model.ReservationNumber);
                command.Parameters.AddWithValue("PersonalNumber", model.PersonalNumber);
                command.Parameters.AddWithValue("DateFrom", model.DateFrom.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("DateTo", model.DateTo.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("Price", price);

                command.ExecuteNonQuery();
                return (int)connection.LastInsertRowId;
            }
        }

        public Reservation GetReservation(long id)
        {
            var sqlQuery = $"SELECT [Id], [HousingId], [ReservationNumber], [PersonalNumber], [DateFrom], [DateTo], [Price] FROM [Reservation] WHERE [Id] = {id}";

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                SQLiteDataReader reader;
                using (var command = new SQLiteCommand(sqlQuery, connection))
                {
                    reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                }

                if (!reader.Read()) return null;

                var reservation = new Reservation
                {
                    Id = (long)reader["Id"],
                    HousingId = (long)reader["HousingId"],
                    ReservationNumber = reader["ReservationNumber"].ToString(),
                    PersonalNumber = reader["PersonalNumber"].ToString(),
                    DateFrom = DateTime.Parse(reader["DateFrom"].ToString()),
                    DateTo = DateTime.Parse(reader["DateTo"].ToString()),
                    Price = (decimal)reader["Price"]
                };

                reader.Close();
                reader.Dispose();

                return reservation;
            }
        }

        public decimal GetPrice(long housingId, DateTime dateFrom, DateTime dateTo)
        {
            var sqlQuery = $"SELECT [BaseDayFee], [DayPriceFactor] FROM [Housing] WHERE [Id] = {housingId}";

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                SQLiteDataReader reader;
                using (var command = new SQLiteCommand(sqlQuery, connection))
                {
                    reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                }

                if (!reader.Read()) return -1;
                var baseDayFee = (long) reader["BaseDayFee"];
                var dayPriceFactor = (decimal) reader["DayPriceFactor"];
                var numberOfDays = dateTo.Subtract(dateFrom).Days;

                reader.Close();
                reader.Dispose();
                
                return baseDayFee * numberOfDays * dayPriceFactor;
            }
        }
    }
}

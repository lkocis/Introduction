using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Introduction.Model;
using Introduction.Repository.Common;
using Npgsql;

namespace Introduction.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private const string connectionString = "Host=localhost:5432;" +
        "Username=postgres;" +
        "Password=postgres;" +
        "Database=postgres";

        public async Task<Reservation> GetReservationById(Guid id)
        {
            var reservation = new Reservation();

            var commandText = $"SELECT r.\"Id\" AS \"Id\", " +
           "r.\"Price\" AS \"Price\", " +
           "r.\"DateFrom\" AS \"DateFrom\", " +
           "r.\"DateTo\" AS \"DateTo\", " +
           "u.\"Email\" AS \"UserEmail\", " +
           "u.\"FirstName\" AS \"UserFirstName\", " +
           "rt.\"Type\" AS \"ReservationType\", " +
           "h.\"Name\" AS \"HotelName\" " +
            "FROM \"Reservation\" r " +
            "INNER JOIN \"User\" u ON r.\"UserId\" = u.\"Id\" " +
            "INNER JOIN \"ReservationType\" rt ON r.\"ReservationTypeId\" = rt.\"Id\" " +
            "INNER JOIN \"HotelReservationType\" hrt ON r.\"ReservationTypeId\" = hrt.\"ReservationTypeId\" " +
            "INNER JOIN \"Hotel\" h ON hrt.\"HotelId\" = h.\"Id\" " +
            "WHERE r.\"Id\" = @id;";

            using var connection = new NpgsqlConnection(connectionString);
            using var command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@id", id);

            try
            {
                await connection.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    reservation.Id = reader.GetGuid(reader.GetOrdinal("Id"));
                    reservation.Price = reader.GetDecimal(reader.GetOrdinal("Price"));
                    reservation.DateFrom = reader.GetDateTime(reader.GetOrdinal("DateFrom"));
                    reservation.DateTo = reader.GetDateTime(reader.GetOrdinal("DateTo"));

                    reservation.User = new User
                    {
                        Email = reader.GetString(reader.GetOrdinal("UserEmail")),
                        FirstName = reader.GetString(reader.GetOrdinal("UserFirstName"))
                    };

                    reservation.ReservationType = new ReservationType
                    {
                        Type = reader.GetString(reader.GetOrdinal("ReservationType"))
                    };

                    reservation.HotelReservationType = new HotelReservationType
                    {
                        Hotel = new Hotel
                        {
                            Name = reader.GetString(reader.GetOrdinal("HotelName"))
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }

            return reservation;
        }
    }
    
}

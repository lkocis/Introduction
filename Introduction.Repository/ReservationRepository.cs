using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Reservation> GetReservationInfo(Guid id)
        {
            Reservation reservation = new Reservation();
            using var connection = new NpgsqlConnection(connectionString);
            var commandText = $"SELECT r.\"Id\" AS \"Id\", " +
                                "r.\"Price\" AS \"Price\", " +
                                "r.\"DateFrom\" AS \"DateFrom\", " +
                                "a.\"DateTo\" AS \"DateTo\" " +
                                "FROM \"Reservation\" r " +
                                "WHERE a.\"Id\"=@id;";

            using var command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@id", id);
            connection.Open();

            using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                reader.Read();
                reservation.Id = Guid.Parse(reader["Id"].ToString());
                reservation.Price = Convert.ToDecimal(reader["Price"]);
                reservation.DateFrom = Convert.ToDateTime(reader["DateFrom"].ToString());
                reservation.DateTo = Convert.ToDateTime(reader["DateTo"].ToString());
            }
            if (reservation == null)
            {
                return reservation;
            }
            return reservation;
        }
    }
}

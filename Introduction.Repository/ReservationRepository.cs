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

        public async Task<bool> PostReservationInfo(Reservation reservation)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                string commandText = $"INSERT INTO\"Reservation\"VALUES(@id, @firstName, @lastName, @DOB);";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.NewGuid());
                command.Parameters.AddWithValue("@userId", NpgsqlTypes.NpgsqlDbType.Uuid, reservation.UserId);
                command.Parameters.AddWithValue("@canceled", reservation.Canceled);
                command.Parameters.AddWithValue("@price", reservation.Price);
                command.Parameters.AddWithValue("@dateFrom", reservation.DateFrom);
                command.Parameters.AddWithValue("@dateTo", reservation.DateTo);
                command.Parameters.AddWithValue("@reservationTypeId", NpgsqlTypes.NpgsqlDbType.Uuid, reservation.ReservationTypeId);
                command.Parameters.AddWithValue("@dateCreated", reservation.DateCreated);
                command.Parameters.AddWithValue("@dateUpdated", reservation.DateUpdated);
                command.Parameters.AddWithValue("@updatedByUserId", reservation.UpdatedByUserId);


                connection.Open();
                var numberOfCommits = await command.ExecuteNonQueryAsync();
                connection.Close();

                if (numberOfCommits == 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

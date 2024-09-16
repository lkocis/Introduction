using Introduction.Model;
using Introduction.Repository.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private const string connectionString = "Host=localhost:5432;" +
        "Username=postgres;" +
        "Password=postgres;" +
        "Database=postgres";
        public async Task<bool> PostRoleAsync(Role role)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                string commandText = $"INSERT INTO\"Role\"VALUES(@id, @name, @dateCreated, @dateUpdated, @createdByUserId, @updatedByUserId);";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.NewGuid());
                command.Parameters.AddWithValue("@name", role.Name);
                command.Parameters.AddWithValue("@dateCreated", role.DateCreated);
                command.Parameters.AddWithValue("@dateUpdated", role.DateUpdated);
                command.Parameters.AddWithValue("@createdByUserId", role.CreatedByUserId);
                command.Parameters.AddWithValue("@updatedByUserId", role.UpdatedByUserId);

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

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
    public class UserRepository : IUserRepository
    {
        private const string connectionString = "Host=localhost:5432;" +
        "Username=postgres;" +
        "Password=postgres;" +
        "Database=postgres";

        public async Task<User> GetUserInfoAsync(Guid id)
        {
            var user = new User();
            using var connection = new NpgsqlConnection(connectionString);
            var commandText = $"SELECT u.\"Id\" AS \"Id\", " +
                    "u.\"RoleId\" AS \"RoleId\", " +
                    "u.\"IsActive\" AS \"IsActive\", " +
                    "u.\"Username\" AS \"Username\", " +
                    "u.\"Password\" AS \"Password\", " +
                    "u.\"FirstName\" AS \"FirstName\", " +
                    "u.\"LastName\" AS \"LastName\", " +
                    "u.\"DateCreated\" AS \"DateCreated\", " +
                    "u.\"DateUpdated\" AS \"DateUpdated\", " +
                    "u.\"Email\" AS \"Email\", " +
                    "u.\"PhoneNumber\" AS \"PhoneNumber\" " +
                    "FROM \"User\" u " +
                    "WHERE u.\"Id\" = @id;";


            using var command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    user.Id = Guid.Parse(reader["Id"].ToString());
                    user.RoleId = Guid.Parse(reader["RoleId"].ToString());
                    user.IsActive = (bool)reader["IsActive"];
                    user.Username = reader["Username"].ToString();
                    user.Password = reader["Password"].ToString();
                    user.FirstName = reader["FirstName"].ToString();
                    user.LastName = reader["LastName"].ToString();
                    user.Email = reader["Email"].ToString();
                    user.PhoneNumber = reader["PhoneNumber"].ToString();
                    user.DateCreated = Convert.ToDateTime(reader["DateCreated"].ToString());
                    user.DateUpdated = reader["DateUpdated"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DateUpdated"]);
                }

            }
            return user;
        }

        /*public async Task<bool> PostUserAsync(User user)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                string commandText = $"INSERT INTO\"User\"VALUES(@id, @roleId, @isActive, @username, @password, @firstName, @lastName, @phoneNumber, @dateCreated, @dateUpdated, @email);";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.NewGuid());
                command.Parameters.AddWithValue("@roleId", NpgsqlTypes.NpgsqlDbType.Uuid, user.RoleId);
                command.Parameters.AddWithValue("@isActive", user.IsActive);
                command.Parameters.AddWithValue("@username", user.Username);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@firstName", user.FirstName);
                command.Parameters.AddWithValue("@lastName", user.LastName);
                command.Parameters.AddWithValue("@phoneNumber", user.PhoneNumber);
                command.Parameters.AddWithValue("@dateCreated", user.DateCreated);
                command.Parameters.AddWithValue("@dateUpdated", user.DateUpdated);
                command.Parameters.AddWithValue("@email", user.Email);

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
        }*/

        
    }
}

using Introduction.Model;
using Introduction.Repository.Common;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Text;

namespace Introduction.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private const string connectionString = "Host=localhost:5432;" +
        "Username=postgres;" +
        "Password=postgres;" +
        "Database=postgres";

        public async Task<bool> PostAuthorAsync(Author author)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                string commandText = $"INSERT INTO\"Author\"VALUES(@id, @firstName, @lastName, @DOB);";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.NewGuid());
                command.Parameters.AddWithValue("@firstName", author.FirstName);
                command.Parameters.AddWithValue("@lastName", author.LastName);
                command.Parameters.AddWithValue("@DOB", author.DOB);

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

        public async Task<bool> DeleteAuthorByIdAsync(Guid id)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                var commandText = "DELETE FROM \"Author\" WHERE\"Id\"=@id;";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", id);

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

        public async Task<Author> GetAuthorByIdAsync(Guid id)
        {
            var author = new Author();
            using var connection = new NpgsqlConnection(connectionString);
            var commandText = "SELECT * FROM \"Author\" WHERE \"Id\" = @id;";
            using var command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {

                reader.Read();
                author.Id = Guid.Parse(reader[0].ToString());
                author.FirstName = reader[1].ToString();
                author.LastName = reader["LastName"].ToString();
                author.DOB = Convert.ToDateTime(reader[3].ToString());

            }

            return author;
        }

        public async Task<bool> PutAuthorByIdAsync(Guid id, Author author)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();

                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                stringBuilder.Append("UPDATE \"Author\" SET ");

                using var command = new NpgsqlCommand();

                if (author.FirstName != null)
                {
                    stringBuilder.Append("\"FirstName\"=@firstName, ");
                    command.Parameters.AddWithValue("@firstName", author.FirstName);
                }

                if (author.LastName != null)
                {
                    stringBuilder.Append("\"LastName\"=@lastName, ");
                    command.Parameters.AddWithValue("@lastName", author.LastName);
                }

                if (author.DOB != null)
                {
                    stringBuilder.Append("\"DOB\"=@dob, ");
                    command.Parameters.AddWithValue("@dob", author.DOB);
                }

                if (stringBuilder.ToString().EndsWith(", "))
                {
                    stringBuilder.Length -= 2;
                }

                stringBuilder.Append(" WHERE \"Id\"=@id;");
                command.Parameters.AddWithValue("@id", id);

                command.CommandText = stringBuilder.ToString();
                command.Connection = connection;

                connection.Open();
                var numberOfCommits = command.ExecuteNonQuery();
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

        public async Task<List<Author>> GetAllAsync()
        {

            Author author = new Author();
            Book book = new Book();
            List<Author> authors = new List<Author>();
            using var connection = new NpgsqlConnection(connectionString);
            var commandText = $"SELECT * FROM \"Book\" LEFT JOIN \"Author\" ON \"Book\".\"AuthorId\" = \"Author\".\"Id\";";
            using var command = new NpgsqlCommand(commandText, connection);

            connection.Open();
            using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    
                    author.Id = Guid.Parse(reader[0].ToString());
                    author.FirstName = reader[1].ToString();
                    author.LastName = reader["LastName"].ToString();
                    author.DOB = Convert.ToDateTime(reader[3].ToString());

                    book.Id = Guid.Parse(reader[4].ToString());
                    book.Title = reader[5].ToString();
                    book.Description = reader["Description"].ToString();
                    book.AuthorId = Guid.TryParse(reader[7].ToString(), out var result) ? result : null;
                    authors.Add(author);
                }
            }

            if (authors == null)
            {
                return null;
            }
            return authors;
        }

    }
}


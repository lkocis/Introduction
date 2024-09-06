using Introduction.Model;
using Introduction.Repository.Common;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Introduction.Repository
{
    public class BookRepository
    {
        IBookRepository bookRepository;

        private const string connectionString = "Host=localhost:5432;" +
        "Username=postgres;" +
        "Password=postgres;" +
        "Database=postgres";

        public bool PostBook(Book book)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                string commandText = $"INSERT INTO\"Book\"VALUES(@id, @Title, @Description);";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.NewGuid());
                command.Parameters.AddWithValue("@Title", book.Title);
                command.Parameters.AddWithValue("@Description", book.Description);

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

        public bool DeleteBookById(Guid id)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                var commandText = "DELETE FROM\"Author\"WHERE\"Id\"=@id;";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", id);

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
    }
}

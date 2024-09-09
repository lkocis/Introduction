using Introduction.Model;
using Introduction.Repository.Common;
using Npgsql;
using System.Text;

namespace Introduction.Repository
{
    public class BookRepository: IBookRepository
    {
        

        private const string connectionString = "Host=localhost:5432;" +
        "Username=postgres;" +
        "Password=postgres;" +
        "Database=postgres";

        public bool PostBook(Book book)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                string commandText = $"INSERT INTO\"Book\"VALUES(@id, @Title, @Description, @AuthorId);";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.NewGuid());
                command.Parameters.AddWithValue("@Title", book.Title);
                command.Parameters.AddWithValue("@Description", book.Description);
                command.Parameters.AddWithValue("@AuthorId", NpgsqlTypes.NpgsqlDbType.Uuid, book.AuthorId);

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
                var commandText = "DELETE FROM \"Book\" WHERE\"Id\"=@id;";
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

        public bool GetBookById(Guid id)
        {
            try
            {
                Book book = new Book();
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = "SELECT * FROM \"Book\" WHERE \"Id\"= @id;";
                using var command = new NpgsqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();

                using NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    book.Id = Guid.Parse(reader[0].ToString());
                    book.Title = reader[1].ToString();
                    book.Description = reader["Description"].ToString();
                    book.AuthorId = Guid.TryParse(reader[3].ToString(), out var result) ? result : null;
                }
                if (book == null)
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

        //ne radi
        public bool PutBookById(Guid id, Book book)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();

                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                stringBuilder.Append("UPDATE \"Book\" SET ");

                using var command = new NpgsqlCommand();

                if (book.Title != null)
                {
                    stringBuilder.Append("\"Title\"=@title, ");
                    command.Parameters.AddWithValue("@title", book.Title);
                }

                if (book.Description != null)
                {
                    stringBuilder.Append("\"Description\"=@description, ");
                    command.Parameters.AddWithValue("@description", book.Description);
                }

                if (book.AuthorId != null)
                {
                    stringBuilder.Append("\"AuthorId\"=@authorId, ");
                    command.Parameters.AddWithValue("@authorId", book.AuthorId);
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
    }
}

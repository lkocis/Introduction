using Introduction.Common;
using Introduction.Model;
using Introduction.Repository.Common;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Buffers;
using System.Globalization;
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
            var commandText = $"SELECT a.\"Id\" AS \"AuthorsId\", " +
                                "a.\"FirstName\" AS \"FirstName\", " +
                                "a.\"LastName\" AS \"LastName\", " +
                                "a.\"DOB\" AS \"DOB\", " +
                                "b.\"Id\" AS \"BookId\", " +
                                "b.\"Title\" AS \"BookTitle\", " +
                                "b.\"Description\" AS \"BookDescription\", " +
                                "b.\"AuthorId\" AS \"AuthorId\" " +
                                "FROM \"Author\" a " +
                                "RIGHT JOIN \"Book\" b ON b.\"AuthorId\" = a.\"Id\" " +
                                "WHERE a.\"Id\"=@id;";

            using var command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                while(await reader.ReadAsync())
                {
                    author.Id = Guid.Parse(reader["AuthorsId"].ToString());
                    author.FirstName = reader["FirstName"].ToString();
                    author.LastName = reader["LastName"].ToString();
                    author.DOB = Convert.ToDateTime(reader["DOB"].ToString());

                    Book book = new Book();
                    book.Id = Guid.Parse(reader["BookId"].ToString());
                    book.Title = reader["BookTitle"].ToString();
                    book.Description = reader["BookDescription"].ToString();
                    book.AuthorId = Guid.TryParse(reader["AuthorId"].ToString(), out var result) ? result : null;
                    author.Books.Add(book);
                }
                
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

                if (!DateTime.Equals(author.DOB, "0001-01-01"))
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

        public async Task<List<Author>> GetAllAsync(AuthorFilter filter, Paging paging, Sorting sorting)
        {
            List<Author> authors = new List<Author>();
            StringBuilder sb = new StringBuilder();
            using var connection = new NpgsqlConnection(connectionString);
            using var command = new NpgsqlCommand();

            sb.Append($"SELECT b.\"Id\" AS \"BookId\", " +
                                "b.\"Title\" AS \"BookTitle\", " +
                                "b.\"Description\" AS \"BookDescription\", " +
                                "b.\"AuthorId\" AS \"AuthorId\", " +
                                "a.\"Id\" AS \"IdOfAuthor\", " +
                                "a.\"FirstName\" AS \"FirstName\", " +
                                "a.\"LastName\" AS \"LastName\", " +
                                "a.\"DOB\" AS \"DOB\" " +
                                "FROM \"Book\" b " +
                                "LEFT JOIN \"Author\" a ON b.\"AuthorId\" = a.\"Id\" WHERE 1=1"
            );


            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.SearchQuery))
                {
                    sb.Append(" AND \"a\".\"FirstName\" LIKE @SearchQuery");
                    command.Parameters.AddWithValue("@SearchQuery", $"%{filter.SearchQuery}%");
                }

                if (!string.IsNullOrWhiteSpace(filter.FirstName))
                {
                    sb.Append(" AND \"a\".\"FirstName\" LIKE @FirstName");
                    command.Parameters.AddWithValue("@FirstName", $"%{filter.FirstName}%");
                }

                if (!string.IsNullOrWhiteSpace(filter.LastName))
                {
                    sb.Append(" AND \"a\".\"LastName\" LIKE @LastName");
                    command.Parameters.AddWithValue("@LastName", $"%{filter.LastName}%");
                }

                if (!string.IsNullOrWhiteSpace(filter.DateOfBirth.ToString()))
                {
                    sb.Append(" AND \"a\".\"DOB\" LIKE @DateOfBirth");
                    command.Parameters.AddWithValue("@DateOfBirth", $"%{filter.DateOfBirth}%");
                }
            }

            if(paging != null)
            {
                if (paging.PageNumber > 0)
                {
                    int authorsPerPage = paging.PageSize;
                    int offsetValue = (paging.PageNumber - 1) * authorsPerPage;

                    sb.Append(" LIMIT @authorsPerPage OFFSET @offsetValue");

                    command.Parameters.AddWithValue("@authorsPerPage", authorsPerPage);
                    command.Parameters.AddWithValue("@offsetValue", offsetValue);
                }
            }

            if (!string.IsNullOrWhiteSpace(sorting.SortBy))
            {
                sb.Append(" ORDER BY ");
                sb.Append($"\"{sorting.SortBy}\"");

                if (!string.IsNullOrEmpty(sorting.SortDirection)
                    && (string.Equals(sorting.SortDirection, "asc", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(sorting.SortDirection, "desc", StringComparison.OrdinalIgnoreCase)))
                {
                    sb.Append($" {sorting.SortDirection.ToUpper()} ");
                }
                else
                {
                    sb.Append(" ASC ");
                }
            }

            command.CommandText = sb.ToString();
            command.Connection = connection;

            connection.Open();
            using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    Book book = new Book();
                    book.Id = Guid.Parse(reader["BookId"].ToString());
                    book.Title = reader["BookTitle"].ToString();
                    book.Description = reader["BookDescription"].ToString();
                    book.AuthorId = Guid.TryParse(reader["AuthorId"].ToString(), out var result) ? result : null;

                    Author author = new Author();
                    author.Id = Guid.Parse(reader["IdOfAuthor"].ToString());
                    author.FirstName = reader["FirstName"].ToString();
                    author.LastName = reader["LastName"].ToString();
                    author.DOB = Convert.ToDateTime(reader["DOB"].ToString());

                    authors.Add(author);
                    author.Books.Add(book);
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


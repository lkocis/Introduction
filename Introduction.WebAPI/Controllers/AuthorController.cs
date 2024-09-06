using Microsoft.AspNetCore.Mvc;
using Npgsql;
using NpgsqlTypes;
using System.Security.Cryptography.X509Certificates;

namespace Introduction.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private const string connectionString = "Host=localhost:5432;" +
        "Username=postgres;" +
        "Password=postgres;" +
        "Database=postgres";

        [HttpPost]
        [Route("PostAuthor/")]
        public ActionResult PostAuthor([FromBody] Author author)
        {
            try 
            {
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                string commandText = $"INSERT INTO\"Author\"VALUES(@id, @firstName, @lastName, @DOB, @bookId);";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.NewGuid());
                command.Parameters.AddWithValue("@firstName", author.FirstName);
                command.Parameters.AddWithValue("@lastName", author.LastName);
                command.Parameters.AddWithValue("@DOB", author.DOB);
                command.Parameters.AddWithValue("@bookId", NpgsqlTypes.NpgsqlDbType.Uuid, author.BookId);

                connection.Open();
                var numberOfCommits = command.ExecuteNonQuery();
                connection.Close();

                if (numberOfCommits == 0)
                {
                    return BadRequest(); 
                }
                return Ok("Succesfully added!");
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteAuthorById/{id}")]
        public ActionResult DeleteAuthorById(Guid id)
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
                    return NotFound();
                }
                return Ok("Succesfully deleted!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }

        [HttpPut]
        [Route("PutAuthorById/{id}/{dob}")]
        public ActionResult PutAuthorById(Guid id, DateOnly dob)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                var commandText = $"UPDATE\"Author\"SET\"DOB\"=@dob WHERE\"Id\"=@id;";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@dob", dob);

                connection.Open();
                var numberOfCommits = command.ExecuteNonQuery();
                connection.Close();

                if (numberOfCommits == 0)
                {
                    return NotFound();
                }
                return Ok("Succesfully updated!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("GetAuthorById/{id}")]
        public ActionResult Get(Guid id)
        {
            try
            {
                var author = new Author();
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = "SELECT * FROM \"Author\" WHERE \"Id\" = @id;";
                using var command = new NpgsqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {

                    reader.Read();
                    author.Id = Guid.Parse(reader[0].ToString());
                    author.FirstName = reader[1].ToString();
                    author.LastName = reader["LastName"].ToString();
                    author.DOB = Convert.ToDateTime(reader[3].ToString());
                    author.BookId = Guid.TryParse(reader[4].ToString(), out var result) ? result : null;
                }
                if (author == null)
                {
                    return NotFound();
                }
                return Ok(author);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
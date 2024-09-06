using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Introduction.WebAPI.Controllers
{
    public class BookController: ControllerBase
    {
        private const string connectionString = "Host=localhost:5432;" +
        "Username=postgres;" +
        "Password=postgres;" +
        "Database=postgres";

        [HttpPost]
        [Route("PostBook/")]
        public IActionResult PostBook([FromBody] Book book)
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
                    return BadRequest(); 
                }
                return Ok("Succesfully added!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

using Introduction.Model;
using Introduction.Service;
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
                AuthorService authorService = new AuthorService();
                bool checker = authorService.PostAuthor(author);

                if (checker == false)
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
                AuthorService authorService = new AuthorService();
                bool checker = authorService.DeleteAuthorById(id);

                if (checker == false)
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
        public ActionResult PutAuthorById(Guid id, [FromBody] Author author)
        {
            try
            {
                AuthorService authorService = new AuthorService();
                bool checker = authorService.PutAuthorById(id, author);
                if (checker == false)
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
        public ActionResult GetAuthorById(Guid id)
        {
            try
            {
                AuthorService authorService = new AuthorService();
                bool checker = authorService.GetAuthorById(id);

                if (checker == false)
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
    }
}
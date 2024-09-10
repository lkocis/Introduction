using Introduction.Model;
using Introduction.Service;
using Introduction.Service.Common;
using Microsoft.AspNetCore.Authorization;
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
        protected IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost]
        [Route("PostAuthor/")]
        public async Task<ActionResult> PostAuthorAsync([FromBody] Author author)
        {
            try 
            {
                bool isSuccessful = await _authorService.PostAuthorAsync(author);

                if (!isSuccessful)
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
        public async Task<ActionResult> DeleteAuthorByIdAsync(Guid id)
        {
            try
            {
                bool isSuccessful = await _authorService.DeleteAuthorByIdAsync(id);

                if (!isSuccessful)
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
        [Route("PutAuthorById/{id}")]
        public async Task<ActionResult> PutAuthorByIdAsync(Guid id, [FromBody] Author author)
        {
            try
            {
                bool isSuccessful = await _authorService.PutAuthorByIdAsync(id, author);
                if (!isSuccessful)
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
        public async Task<ActionResult> GetAuthorByIdAsync(Guid id)
        {
            try
            {
                Author author = await _authorService.GetAuthorByIdAsync(id);

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

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult> GetAllAsync()
        {
            try
            {
                List<Author> authors = await _authorService.GetAllAsync();

                if (authors == null)
                {
                    return NotFound();
                }
                return Ok(authors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
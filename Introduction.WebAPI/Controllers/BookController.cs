using Introduction.Model;
using Introduction.Service;

using Microsoft.AspNetCore.Mvc;

namespace Introduction.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController: ControllerBase
    {
        [HttpPost]
        [Route("PostBook/")]
        public async Task<ActionResult> PostBookAsync([FromBody] Book book)
        {
            try
            {
                BookService bookService = new BookService();
                var checker = await bookService.PostBookAsync(book);
                if ( checker == false)
                    return BadRequest("Book not posted");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteBookById/{id}")]
        public async Task<ActionResult> DeleteBookByIdAsync(Guid id)
        {
            try
            {
                BookService bookService = new BookService();
                var checker = await bookService.DeleteBookByIdAsync(id);
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

        [HttpGet]
        [Route("GetBookById/{id}")]
        public async Task<ActionResult> GetBookByIdAsync(Guid id)
        {
            try
            {
                BookService bookService = new BookService();
                var checker = await bookService.GetBookByIdAsync(id);
                if(checker == false)
                {
                    return NotFound();
                }
                return Ok("Ok");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("PutBookById/{id}")]
        public async Task<ActionResult> PutBookByIdAsync(Guid id, [FromBody] Book book)
        {
            try
            {
                BookService bookService = new BookService();
                var checker = await bookService.PutBookByIdAsync(id, book);

                if(checker == false)
                {
                    return NotFound();
                }
                return Ok("Book updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}

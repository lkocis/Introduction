using Introduction.Model;
using Introduction.Service;
using Introduction.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace Introduction.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController: ControllerBase
    {
        protected IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }


        [HttpPost]
        [Route("PostBook/")]
        public async Task<ActionResult> PostBookAsync([FromBody] Book book)
        {
            try
            {
                var isSuccessful = await _bookService.PostBookAsync(book);
                if (!isSuccessful)
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
                var isSuccessful = await _bookService.DeleteBookByIdAsync(id);
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

        [HttpGet]
        [Route("GetBookById/{id}")]
        public async Task<ActionResult> GetBookByIdAsync(Guid id)
        {
            try
            {
                Book book = await _bookService.GetBookByIdAsync(id);
                if(book == null)
                {
                    return NotFound();
                }
                return Ok(book);
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
                var isSuccessful = await _bookService.PutBookByIdAsync(id, book);

                if(!isSuccessful)
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

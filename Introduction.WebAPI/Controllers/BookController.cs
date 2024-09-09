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
        public ActionResult PostBook([FromBody] Book book)
        {
            try
            {
                BookService bookService = new BookService();
                var checker = bookService.PostBook(book);
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
        public ActionResult DeleteBookById(Guid id)
        {
            try
            {
                BookService bookService = new BookService();
                var checker = bookService.DeleteBookById(id);
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
        public ActionResult GetBookById(Guid id)
        {
            try
            {
                BookService bookService = new BookService();
                var checker = bookService.GetBookById(id);
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
        public ActionResult PutBookById(Guid id, [FromBody] Book book)
        {
            try
            {
                BookService bookService = new BookService();
                var checker = bookService.PutBookById(id, book);

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

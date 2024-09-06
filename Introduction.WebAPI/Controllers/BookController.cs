using Introduction.Model;
using Introduction.Service;
using Introduction.Service.Common;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Introduction.WebAPI.Controllers
{
    public class BookController: ControllerBase
    {
        [HttpPost]
        [Route("PostBook/")]
        public IActionResult PostBook([FromBody] Book book)
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

        
    }
}

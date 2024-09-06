using Introduction.Model;
using Introduction.Repository;
using Introduction.Service.Common;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Introduction.Service
{
    public class BookService
    {
        public IBookService bookService {  get; set; } 

        public bool PostBook(Book book)
        {
            BookService bookService = new BookService();
            bool checker = bookService.PostBook(book);
            if (checker == false)
            {
                return false;
            }
            return true;

        }

        public bool DeleteBookById(Guid id)
        {
            BookService bookService = new BookService();
            bool checker = bookService.DeleteBookById(id);
            if (checker == false)
            {
                return false;
            }
            return true;
        }
    }
}

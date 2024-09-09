using Introduction.Model;
using Introduction.Repository;
using Introduction.Service.Common;

namespace Introduction.Service
{
    public class BookService : IBookService
    {
       
        public bool PostBook(Book book)
        {
            BookRepository bookRepository = new BookRepository();
            bool checker = bookRepository.PostBook(book);
            if (checker == false)
            {
                return false;
            }
            return true;

        }

        public bool DeleteBookById(Guid id)
        {
            BookRepository bookRepository = new BookRepository();
            bool checker = bookRepository.DeleteBookById(id);
            if (checker == false)
            {
                return false;
            }
            return true;
        }

        public bool GetBookById(Guid id)
        {
            BookRepository bookRepository = new BookRepository();
            bool checker = bookRepository.GetBookById(id);
            if (checker == false)
            {
               return false;
            }  
            return true;
        }

        public bool PutBookById(Guid id, Book book)
        {
            BookRepository bookRepository = new BookRepository();
            bool checker = bookRepository.PutBookById(id, book);
            if (checker == false)
            {
                return false;
            }
            return true;
        }
    }
}

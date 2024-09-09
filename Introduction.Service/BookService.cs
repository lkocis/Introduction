using Introduction.Model;
using Introduction.Repository;
using Introduction.Service.Common;

namespace Introduction.Service
{
    public class BookService : IBookService
    {

        public async Task<bool> PostBookAsync(Book book)
        {
            BookRepository bookRepository = new BookRepository();
            bool checker = await bookRepository.PostBookAsync(book);
            if (checker == false)
            {
                return false;
            }
            return true;

        }

        public async Task<bool> DeleteBookByIdAsync(Guid id)
        {
            BookRepository bookRepository = new BookRepository();
            bool checker = await bookRepository.DeleteBookByIdAsync(id);
            if (checker == false)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> GetBookByIdAsync(Guid id)
        {
            BookRepository bookRepository = new BookRepository();
            bool checker = await bookRepository.GetBookByIdAsync(id);
            if (checker == false)
            {
               return false;
            }  
            return true;
        }

        public async Task<bool> PutBookByIdAsync(Guid id, Book book)
        {
            BookRepository bookRepository = new BookRepository();
            bool checker = await bookRepository.PutBookByIdAsync(id, book);
            if (checker == false)
            {
                return false;
            }
            return true;
        }
    }
}

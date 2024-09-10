using Introduction.Model;
using Introduction.Repository;
using Introduction.Repository.Common;
using Introduction.Service.Common;

namespace Introduction.Service
{
    public class BookService : IBookService
    {
        protected IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<bool> PostBookAsync(Book book)
        {
            bool isSuccessful = await _bookRepository.PostBookAsync(book);
            
            return isSuccessful;

        }

        public async Task<bool> DeleteBookByIdAsync(Guid id)
        {
            bool isSuccessful = await _bookRepository.DeleteBookByIdAsync(id);
            
            return isSuccessful;
        }

        public async Task<Book> GetBookByIdAsync(Guid id)
        {
            Book book = await _bookRepository.GetBookByIdAsync(id);
            
            return book;
        }

        public async Task<bool> PutBookByIdAsync(Guid id, Book book)
        {
            bool isSuccessful = await _bookRepository.PutBookByIdAsync(id, book);
            
            return isSuccessful;
        }
    }
}

using Introduction.Model;

namespace Introduction.Service.Common
{
    public interface IBookService
    {
        bool PostBook(Book book);
        bool DeleteBookById(Guid id);
        bool GetBookById(Guid id);
        bool PutBookById(Guid id, Book book);
    }
}

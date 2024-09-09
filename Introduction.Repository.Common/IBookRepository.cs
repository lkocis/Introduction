using Introduction.Model;

namespace Introduction.Repository.Common
{
    public interface IBookRepository
    {
        bool PostBook(Book book);
        bool DeleteBookById(Guid id);
        bool GetBookById(Guid id);
        bool PutBookById(Guid id, Book book);
    }
}

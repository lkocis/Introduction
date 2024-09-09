using Introduction.Model;

namespace Introduction.Repository.Common
{
    public interface IBookRepository
    {
        Task<bool> PostBookAsync(Book book);
        Task<bool> DeleteBookByIdAsync(Guid id);
        Task<bool> GetBookByIdAsync(Guid id);
        Task<bool> PutBookByIdAsync(Guid id, Book book);
    }
}

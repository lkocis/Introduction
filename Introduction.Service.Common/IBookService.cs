using Introduction.Model;

namespace Introduction.Service.Common
{
    public interface IBookService
    {
        Task<bool> PostBookAsync(Book book);
        Task<bool> DeleteBookByIdAsync(Guid id);
        Task<bool> GetBookByIdAsync(Guid id);
        Task<bool> PutBookByIdAsync(Guid id, Book book);
    }
}

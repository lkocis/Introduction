using Introduction.Model;

namespace Introduction.Repository.Common
{
    public interface IAuthorRepository
    {
        Task<bool> PostAuthorAsync(Author author);
        Task<bool> DeleteAuthorByIdAsync(Guid id);
        Task<bool> GetAuthorByIdAsync(Guid id);
        Task<bool> PutAuthorByIdAsync(Guid id, Author author);
    }
}

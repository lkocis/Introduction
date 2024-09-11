using Introduction.Common;
using Introduction.Model;

namespace Introduction.Repository.Common
{
    public interface IAuthorRepository
    {
        Task<bool> PostAuthorAsync(Author author);
        Task<bool> DeleteAuthorByIdAsync(Guid id);
        Task<Author> GetAuthorByIdAsync(Guid id);
        Task<bool> PutAuthorByIdAsync(Guid id, Author author);
        Task<List<Author>> GetAllAsync(AuthorFilter filter, Paging paging, Sorting sorting);
    }
}

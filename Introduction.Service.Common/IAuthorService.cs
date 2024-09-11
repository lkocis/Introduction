using Introduction.Common;
using Introduction.Model;
using System.Threading.Tasks;

namespace Introduction.Service.Common
{
    public interface IAuthorService
    {
        Task<bool> PostAuthorAsync(Author author);
        Task<bool> DeleteAuthorByIdAsync(Guid id);
        Task<Author> GetAuthorByIdAsync(Guid id);
        Task<bool> PutAuthorByIdAsync(Guid id, Author author);
        Task<List<Author>> GetAllAsync(AuthorFilter filter, Paging paging, Sorting sorting);
    }
}

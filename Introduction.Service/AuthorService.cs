
using Introduction.Model;
using Introduction.Repository;
using Introduction.Service.Common;

namespace Introduction.Service
{
    public class AuthorService : IAuthorService
    {
        public async Task<bool> PostAuthorAsync(Author author)
        {
            AuthorRepository authorRepository = new AuthorRepository();
            bool checker = await authorRepository.PostAuthorAsync(author);
            if (checker == false)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteAuthorByIdAsync(Guid id)
        {
            AuthorRepository authorRepository = new AuthorRepository();
            bool checker = await authorRepository.DeleteAuthorByIdAsync(id);
            if (checker == false)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> GetAuthorByIdAsync(Guid id)
        {
            AuthorRepository authorRepository = new AuthorRepository();
            bool checker = await authorRepository.GetAuthorByIdAsync(id);
            if (checker == false)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> PutAuthorByIdAsync(Guid id, Author author)
        {
            AuthorRepository authorRepository = new AuthorRepository();
            bool checker = await authorRepository.PutAuthorByIdAsync(id, author);
            if (checker == false)
            {
                return false;
            }
            return true;
        }
    }
}

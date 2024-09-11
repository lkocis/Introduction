
using Introduction.Common;
using Introduction.Model;
using Introduction.Repository;
using Introduction.Repository.Common;
using Introduction.Service.Common;

namespace Introduction.Service
{
    public class AuthorService : IAuthorService
    {
        protected IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<bool> PostAuthorAsync(Author author)
        {
            bool isSuccessful = await _authorRepository.PostAuthorAsync(author);
            
            return isSuccessful;
        }

        public async Task<bool> DeleteAuthorByIdAsync(Guid id)
        {
            bool isSuccessful = await _authorRepository.DeleteAuthorByIdAsync(id);
           
            return isSuccessful;
        }

        public async Task<Author> GetAuthorByIdAsync(Guid id)
        { 
            Author author = await _authorRepository.GetAuthorByIdAsync(id);
            
            return author;
        }

        public async Task<bool> PutAuthorByIdAsync(Guid id, Author author)
        {
            bool isSuccessful = await _authorRepository.PutAuthorByIdAsync(id, author);
            
            return isSuccessful;
        }

        public async Task<List<Author>> GetAllAsync(AuthorFilter filter, Paging paging, Sorting sorting)
        {
            List<Author> authors = await _authorRepository.GetAllAsync(filter, paging, sorting);

            return authors;
        }
    }
}

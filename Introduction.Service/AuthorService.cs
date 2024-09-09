
using Introduction.Model;
using Introduction.Repository;
using Introduction.Service.Common;

namespace Introduction.Service
{
    public class AuthorService : IAuthorService
    {
        public bool PostAuthor(Author author)
        {
            AuthorRepository authorRepository = new AuthorRepository();
            bool checker = authorRepository.PostAuthor(author);
            if (checker == false)
            {
                return false;
            }
            return true;
        }

        public bool DeleteAuthorById(Guid id)
        {
            AuthorRepository authorRepository = new AuthorRepository();
            bool checker = authorRepository.DeleteAuthorById(id);
            if (checker == false)
            {
                return false;
            }
            return true;
        }

        public bool GetAuthorById(Guid id)
        {
            AuthorRepository authorRepository = new AuthorRepository();
            bool checker = authorRepository.GetAuthorById(id);
            if (checker == false)
            {
                return false;
            }
            return true;
        }

        public bool PutAuthorById(Guid id, Author author)
        {
            AuthorRepository authorRepository = new AuthorRepository();
            bool checker = authorRepository.PutAuthorById(id, author);
            if (checker == false)
            {
                return false;
            }
            return true;
        }
    }
}

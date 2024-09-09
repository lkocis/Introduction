using Introduction.Model;

namespace Introduction.Repository.Common
{
    public interface IAuthorRepository
    {
        bool PostAuthor(Author author);
        bool DeleteAuthorById(Guid id);
        bool GetAuthorById(Guid id);
        bool PutAuthorById(Guid id, Author author);
    }
}

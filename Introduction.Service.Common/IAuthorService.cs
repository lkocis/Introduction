using Introduction.Model;

namespace Introduction.Service.Common
{
    public interface IAuthorService
    {
        bool PostAuthor(Author author);
        bool DeleteAuthorById(Guid id);
        bool GetAuthorById(Guid id);
        bool PutAuthorById(Guid id, Author author);
    }
}

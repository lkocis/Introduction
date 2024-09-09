using Introduction.Repository.Common;

namespace Introduction.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private const string connectionString = "Host=localhost:5432;" +
        "Username=postgres;" +
        "Password=postgres;" +
        "Database=postgres";

    }
}

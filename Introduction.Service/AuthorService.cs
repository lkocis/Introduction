using Introduction.Service.Common;
using Microsoft.AspNetCore.Authorization;

namespace Introduction.Service
{
    public class AuthorService
    {
        public IAuthorService authorService { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Common
{
    public class AuthorFilter
    {
        public string SearchQuery {  get; set; }
        public Guid? AuthorId { get; set; }
        public string? FirstName {  get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}

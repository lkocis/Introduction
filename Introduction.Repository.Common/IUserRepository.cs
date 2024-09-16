using Introduction.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Repository.Common
{
    public interface IUserRepository
    {
        Task<User> GetUserInfoAsync(Guid id);
        //Task<bool> PostUserAsync(User user);
    }
}

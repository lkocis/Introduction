using Introduction.Model;
using Introduction.Repository.Common;
using Introduction.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Service
{
    public class UserService : IUserService
    {
        protected IUserRepository _userRepository;
        
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> GetUserInfoAsync(Guid id)
        {
            User user = await _userRepository.GetUserInfoAsync(id);
            return user;
        }
    }
}

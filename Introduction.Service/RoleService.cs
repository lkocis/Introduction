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
    public class RoleService : IRoleService
    {
        protected IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<bool> PostRoleAsync(Role role)
        {
            bool isSuccessful = await _roleRepository.PostRoleAsync(role);
            return isSuccessful;
        }
    }
}

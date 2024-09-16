using Introduction.Model;
using Introduction.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace Introduction.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        protected IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpPost]
        [Route("PostRole/")]
        public async Task<ActionResult> PostRoleAsync([FromBody] Role role)
        {
            try
            {
                bool isSuccessful = await _roleService.PostRoleAsync(role);

                if (!isSuccessful)
                {
                    return BadRequest();
                }
                return Ok("Succesfully added!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

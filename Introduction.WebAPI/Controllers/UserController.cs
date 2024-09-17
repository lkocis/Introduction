using Introduction.Model;
using Introduction.Service;
using Introduction.Service.Common;
using Introduction.WebAPI.REST;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Introduction.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        protected IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("GetUser/{id}")]
        public async Task<ActionResult> GetUserInfoAsync(Guid id)
        {
            try
            {
                User user = await _userService.GetUserInfoAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                var userREST = new UserREST
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                };

                return Ok(userREST);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

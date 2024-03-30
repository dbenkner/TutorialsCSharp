using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleJWT.Models;
using SimpleJWT.Services;

namespace SimpleJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login(User user)
        {
            var token = _userService.Login(user);

            if (token == null || token == string.Empty)
            {
                return BadRequest(new { Message = "Invlaid username or password" });
            }
            return Ok(token);
        }
    }
}

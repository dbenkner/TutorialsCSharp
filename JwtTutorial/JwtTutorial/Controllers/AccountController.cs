using AutoMapper;
using JwtLibrary.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using JwtLibrary;
using JwtLibrary.ResponseRequestModels;
using System.Linq.Expressions;
using JwtTutorial.Entities;
using JwtLibrary.Entities;


namespace JwtTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController: ControllerBase
    {
        private readonly IAccountService<ApplicationUser> _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService<ApplicationUser> accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestViewModel model)
        {
            try
            {
                var result = await _accountService.Login(model);
                if (result.IsSuccessful == true)
                {
                    return Ok(result);
                }
                return BadRequest("Not Found");
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Signup")]
        public async Task<IActionResult> SignUp(Customer model)
        {
            try
            {
                var res = await _accountService.SignUp(model, model.PasswordHash);
                return Ok("UserCreaated Successfully");
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefeshToken(TokenModel model)
        {
            try
            {
                var res = await _accountService.RefreshToken(model);
                if (res.RefreshToken != null && res.AccessToken != null)
                {
                    return Ok(res); 
                }
                return BadRequest("Unable To Refresh");
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

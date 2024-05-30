using jwtAuth.DTOs;
using jwtAuth.Models;
using jwtAuth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace jwtAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly jwtDbContext _context;

        public UsersController(jwtDbContext context)
        {
            _context = context;
        }
        [HttpPost("CreateUser")]
        public async Task<ActionResult<User>> CreateNewUser(NewUser newUser)
        {
            if (newUser == null) { return BadRequest(); }
            if (await UsernameExists(newUser.Username)) return BadRequest("User Exists!");
            using var hmac = new HMACSHA256();
            var user = new User
            {
                Username = newUser.Username,
                Email = newUser.Email,
                Name = newUser.Name,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(newUser.Password)),
                PasswordSalt = hmac.Key
            };
            await _context.Users.AddAsync(user);
  
            await _context.SaveChangesAsync();
            var userToRoles = new UserToRole { RoleId = 1, UserId = user.Id };

            await _context.UsersToRoles.AddAsync(userToRoles);
            await _context.SaveChangesAsync();
            return Ok(user);
        }
        [HttpPost("login")]
        public async Task<ActionResult<AuthService>> LogIn(AuthService service, LoginDto loginDto)
        {
            if (loginDto == null) { return BadRequest(); }
            var user = await _context.Users.Include(u => u.Roles).ThenInclude(r => r.Role).FirstOrDefaultAsync(x => x.Username == loginDto.Username);
            if (user == null) { return BadRequest("Invalid Login"); }
            using var hmac = new HMACSHA256(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for(var i = 0; i < computedHash.Length; i++)
            {
                if (user.PasswordHash[i] != computedHash[i])
                {
                    return BadRequest();
                }
            }
            return Ok(service.Create(user));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            if (id  <= 0) { return BadRequest(); }
            var user = await _context.Users.Where(x => x.Id == id).Include(u => u.Roles).ThenInclude(r => r.Role).FirstOrDefaultAsync();
            if (user == null) return NotFound();
            return user;
        }
        [HttpPost("setuserroles/{userId}/{roleId}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> SetUserRole(int userId, int roleId)
        {
            if (userId <= 0 || roleId == 0) return BadRequest();
            var userToRole = new UserToRole { RoleId = roleId, UserId = userId };
            await _context.UsersToRoles.AddAsync(userToRole);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private async Task<bool> UsernameExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.Username == username);
        }
    }
}

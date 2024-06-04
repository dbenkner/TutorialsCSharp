using jwtAuth.DTOs;
using jwtAuth.Models;
using jwtAuth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

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
            if (!ValidateEmail(newUser.Email)) return BadRequest("Invalid Username or Password");
            if (!ValidatePassword(newUser.Password)) return BadRequest("Invalid Username or Password");
            using HMACSHA256 hmac = new HMACSHA256();
            User user = new User
            {
                Username = newUser.Username.ToLower(),
                Email = newUser.Email.ToLower(),
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
        [HttpPut("resetpassword")]
        public async Task<ActionResult<User>> ResetPassword(ResetPwDTO resetPwDTO)
        {
            if (resetPwDTO == null) { return NotFound(); }
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == resetPwDTO.userId);
            if (user == null) { return NotFound(); }
            if (!ValidatePassword(resetPwDTO.NewPassword)) return BadRequest("Invalid");
            using HMACSHA256 hmac = new HMACSHA256(user.PasswordSalt);
            Byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(resetPwDTO.OldPassword));
            for (int i = 0; i < computedHash.Length; ++i)
            {
                if (user.PasswordHash[i] != computedHash[i])
                {
                    return BadRequest("Invalid Login!");
                }
            }
            using HMACSHA256 newHmac = new HMACSHA256();
            user.PasswordHash = newHmac.ComputeHash(Encoding.UTF8.GetBytes(resetPwDTO.NewPassword));
            user.PasswordSalt = newHmac.Key;
            _context.Entry(user).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }catch (DbUpdateConcurrencyException) {
                if (await UserExists(user.Id) == false) { return NotFound(); }    
                else { throw; }
            }
            return NoContent();
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
            User? user = await _context.Users.Where(x => x.Id == id).Include(u => u.Roles).ThenInclude(r => r.Role).FirstOrDefaultAsync();
            if (user == null) return NotFound();
            return user;
        }
        [HttpPost("setuserroles/{userId}/{roleId}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> SetUserRole(int userId, int roleId)
        {
            if (userId <= 0 || roleId == 0) return BadRequest();
            UserToRole userToRole = new UserToRole { RoleId = roleId, UserId = userId };
            await _context.UsersToRoles.AddAsync(userToRole);
            await _context.SaveChangesAsync();
            return Ok(userToRole);
        }
        [HttpDelete("removeuserroles/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> RemoveUserRole(int id)
        {
            if(id <= 0)
            {
                return BadRequest();
            }
            UserToRole? userToRole = await _context.UsersToRoles.FirstOrDefaultAsync(x => x.Id == id);
            if(userToRole == null) return NotFound();
             _context.Remove(userToRole);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private async Task<bool> UsernameExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.Username == username);
        }
        private bool ValidateEmail(string email)
        {
            if(email == null) return false;
            Regex reg = new Regex("^([A-Za-z0-9+-_~.%]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,4})$");
            return reg.IsMatch(email);
        }
        private bool ValidatePassword(string password)
        {
            if(password == null) return false;
            Regex reg = new Regex(@"(?=.*[!@#$%^&*()_+=-?/.,<>])(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{8,32}");
            return reg.IsMatch(password);
        }
        private async Task<bool> UserExists(int id)
        {
            return await _context.Users.AnyAsync(x => x.Id == id); 
        }
    }
}

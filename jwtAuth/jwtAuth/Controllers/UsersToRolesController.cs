using jwtAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace jwtAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersToRolesController : ControllerBase
    {
        private readonly jwtDbContext _context;
        public UsersToRolesController(jwtDbContext context) { 
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles ="admin")]
        public async Task<ActionResult<IEnumerable<UserToRole>>> GetAllUsersToRoles()
        {
            return await _context.UsersToRoles.ToListAsync();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Grimoire_Csharp.Data;
using Grimoire_Csharp.Models;
using Grimoire_Csharp.Services;
using System.Threading.Tasks;

namespace Grimoire_Csharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthService _authService;

        public UsersController(ApplicationDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        [HttpPost("signup")]
        public async Task<ActionResult<User>> Signup(User user)
        {
            user.Password = _authService.HashPassword(user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(User user)
        {
            var existingUser = await _context.Users.SingleOrDefaultAsync(u => u.Email == user.Email);
            if (existingUser == null || !_authService.VerifyPassword(user.Password, existingUser.Password))
            {
                return Unauthorized();
            }

            var token = _authService.GenerateJwtToken(existingUser);
            return Ok(token);
        }
    }
}
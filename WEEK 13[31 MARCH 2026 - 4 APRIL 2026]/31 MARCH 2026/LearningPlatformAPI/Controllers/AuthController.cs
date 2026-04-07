using Microsoft.AspNetCore.Mvc;
using LearningPlatformAPI.Data;
using LearningPlatformAPI.Models;
using LearningPlatformAPI.Services;

namespace LearningPlatformAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(AppDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        // ✅ REGISTER
        [HttpPost("register")]
public IActionResult Register(RegisterDto dto)
{
    var user = new User
    {
        Username = dto.Username,
        Email = dto.Email,
        Password = dto.Password,
        Role = dto.Role
    };

    _context.Users.Add(user);
    _context.SaveChanges();

    return Ok("User Registered");
}

        // ✅ LOGIN
        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user == null)
                return Unauthorized("Invalid credentials");

            var token = _jwtService.GenerateToken(user.Username, user.Role);

            return Ok(new { token });
        }
    }
}
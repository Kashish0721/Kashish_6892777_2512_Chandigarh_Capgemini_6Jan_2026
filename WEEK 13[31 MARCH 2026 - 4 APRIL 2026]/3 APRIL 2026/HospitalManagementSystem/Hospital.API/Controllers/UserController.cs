using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public UserController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // REGISTER
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDTO dto)
    {
        var user = _mapper.Map<User>(dto);

        user.Role = "Patient"; // default role

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok("User Registered");
    }

    // GET ALL USERS
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _context.Users.ToListAsync();

        var result = _mapper.Map<List<UserDTO>>(users);

        return Ok(result);
    }
}
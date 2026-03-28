using Microsoft.AspNetCore.Mvc;
using log4net;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private static readonly ILog log = LogManager.GetLogger(typeof(UserController));

    [HttpPost("login")]
    public IActionResult Login(string email, string password)
    {
        log.Info($"Login attempt: {email}");

        if (password != "1234")
        {
            log.Warn("Invalid password");
            return Unauthorized();
        }

        try
        {
            return Ok("Login successful");
        }
        catch (Exception ex)
        {
            log.Error("Exception occurred during login", ex);
            return StatusCode(500);
        }
    }
}
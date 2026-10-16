using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace raceday_part2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (request.Email != "tester" || request.Password != "password")
            return Unauthorized(new { message = "Invalid email or password." });

        return Ok(new { message = "Login successful." });
    }

    public class LoginRequest
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }

    }
}

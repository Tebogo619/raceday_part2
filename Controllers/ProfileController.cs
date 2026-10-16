using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace raceday_part2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProfile()
        {
            // For demonstration purposes, returning a static profile.
            var profile = new
            {
                Id = 1,
                Name = "John Doe",
                Email = "john.doe@example.com"
            };
            return Ok(profile);
        }
    }
}

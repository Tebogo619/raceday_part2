using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace raceday_part2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetResults()
        {
            // For demonstration purposes, returning a static list of results.
            var results = new[]
            {
                new { EventId = 1, Participant = "Alice", Position = 1 },
                new { EventId = 1, Participant = "Bob", Position = 2 },
                new { EventId = 2, Participant = "Charlie", Position = 1 },
                new { EventId = 2, Participant = "David", Position = 2 }
            };
            return Ok(results);
        }
    }
}

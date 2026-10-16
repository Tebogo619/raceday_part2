using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace raceday_part2.Controllers
{    [Route("api/[controller]")]
    [ApiController]
    public class EnrolmentController : ControllerBase
    {

    private readonly RaceDayContext _context;

        public EnrolmentController(RaceDayContext context)
        {
            _context = context;
        }

        // GET: api/Enrolment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrolment>>> GetEnrolments()
        {
            return await _context.Enrolments.ToListAsync();
        }

        // GET: api/Enrolment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Enrolment>> GetEnrolment(int id)
        {
            var enrolment = await _context.Enrolments.FindAsync(id);

            if (enrolment == null)
                return NotFound();

            return enrolment;
        }

        // POST: api/Enrolment
        [HttpPost]
        public async Task<ActionResult<Enrolment>> CreateEnrolment(Enrolment enrolment)
        {
            _context.Enrolments.Add(enrolment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetEnrolment),
                new { id = enrolment.EnrolmentID },
                enrolment);
        }

        // DELETE: api/Enrolment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrolment(int id)
        {
            var enrolment = await _context.Enrolments.FindAsync(id);

            if (enrolment == null)
                return NotFound();

            _context.Enrolments.Remove(enrolment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

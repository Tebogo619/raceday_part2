using System.ComponentModel.DataAnnotations;

namespace raceday_part2.Models
{
    public class Enrolment
    {
        public int EnrollmentID { get; set; }
        public int ParticipantID { get; set; }
        public int CategoryID { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
        [Required, StringLength(100)]
        public string Status { get; set; } = "Pending";
        public Participant Participant { get; set; }    
        public Category Category { get; set; }
        public Result Results { get; set; }

    }
}

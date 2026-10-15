using System.ComponentModel.DataAnnotations;

namespace raceday_part2.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public int EventID { get; set; }
        [Required, StringLength(100)] public string CategoryName { get; set; } = string.Empty;
        public decimal EntryFee { get; set; }
        public decimal DistanceKm { get; set; }
        public int MaxParticipants { get; set; }
        public  Event Event { get; set; }
        public ICollection<Enrolment> Enrollments { get; set; } = new List<Enrolment>();

    }
}

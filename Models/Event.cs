using System.ComponentModel.DataAnnotations;

namespace raceday_part2.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public int OrganiserId { get; set; }
        [Required, StringLength(150)] public string EventName { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        [Required, StringLength(200)] public string Location { get; set; }
        [StringLength(200)] public string Description { get; set; }
        public decimal DistanceKm { get; set; }
        [Required, StringLength(100)] public string EventType { get; set; } = "Run";
        [Required, StringLength(20)] public string Status { get; set; } = "Scheduled";
        public Organiser Organiser { get; set; }
        public ICollection<Category> Categories { get; set; } = new List<Category>();



    }
}

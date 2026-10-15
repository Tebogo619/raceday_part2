using System.ComponentModel.DataAnnotations;
namespace raceday_part2.DTOs
{
    public class Requests
    {
        [Required] public string Role { get; set; } = "Participants";
        [Required, StringLength(100)] public string Name { get; set; } = string.Empty;
        [Required, StringLength(100)] public string Email { get; set; } = string.Empty;
        [Required, StringLength(15)] public string MinLength { get; set; } = string.Empty;
        [Required, StringLength(256)] public string PhoneNumber { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string EmergencyContact { get; set; }
    }
    public class LoginRequest
    {
        [Required, StringLength(100)] public string Email { get; set; } = string.Empty;
        [Required, StringLength(256)] public string Password { get; set; } = string.Empty; }

        public class UpdateProfileRequest
        {
            [Required, StringLength(100)] public string Name { get; set; } = string.Empty;
            [Required, StringLength(100)] public string Email { get; set; } = string.Empty;
            [Required, StringLength(15)] public string PhoneNumber { get; set; } = string.Empty;
            public DateTime? DateOfBirth { get; set; }
            public string EmergencyContact { get; set; }
        }
    public class EventRequest
    {
        [Required, StringLength(100)] public string EventName { get; set; } = string.Empty;
        [Required] public DateTime EventDate { get; set; }
        [Required, StringLength(200)] public string Location { get; set; } = string.Empty;
        [Required] public int OrganiserID { get; set; } 
        public string Locations { get; set; } = string.Empty;
        public decimal DistanceKm { get; set; }
        public string EventType { get; set; } = "Run";
        public string Status { get; set; } = "Scheduled";
    }
    public class CategoryRequest
    {
        [Required, StringLength(50)] public string CategoryName { get; set; } = string.Empty; [Range(0.01, 1000)]
        public decimal DistanceKm { get; set; }
        [Range(1, 100000)] public int MaxParticipants { get; set; } = 100; [Range(0, 10000000)]
        public decimal EntryFee { get; set; }
    }
        public class ResultRequest
        {
        public TimeSpan FinishTime { get; set; }
        public int Position { get; set; }  
        public string Status { get; set; } = "Completed";

    }
}

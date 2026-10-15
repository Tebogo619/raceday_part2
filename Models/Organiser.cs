using System.ComponentModel.DataAnnotations;

namespace raceday_part2.Models
{
    public class Organiser
    {
        public int OrganiserID { get; set; }
        [Required, StringLength(100)] public string OrganiserName { get; set; } = string.Empty;
        [Required, StringLength(100)] public string Email { get; set; } = string.Empty;
        [Required, StringLength(15)] public string PhoneNumber { get; set; } = string.Empty;
        [Required, StringLength(256)] public string PasswordHashing { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<Event> Events { get; set; } = new List<Event>();

    }
}

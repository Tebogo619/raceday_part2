namespace raceday_part2.Models
{
    public class Result
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int ParticipantId { get; set; }
        public TimeSpan FinishTime { get; set; }
        public DateTime FinishDate { get; set; }
        // Navigation properties
        public Event Event { get; set; }
        public Participant Participant { get; set; }
        public Enrolment Enrollment { get; set; }
    }
}

namespace CenterDragon.Models.Entites
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Content { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Instractor? instractor { get; set; }
        public int? instractorId { get; set; }

        public virtual List<Participants> Participants { get; set; } = new List<Participants>();
    }
}

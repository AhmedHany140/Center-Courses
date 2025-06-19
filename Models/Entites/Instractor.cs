namespace CenterDragon.Models.Entites
{
    public class Instractor
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? About { get; set; }

        public Course? Course { get; set; }
     

        public virtual List <Participants> Participants { get; set; }=new List<Participants>();


    }
}

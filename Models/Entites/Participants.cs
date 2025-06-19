using System.ComponentModel.DataAnnotations;

namespace CenterDragon.Models.Entites
{
    public class Participants
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public int? Age { get; set; }
        public string? Ediation { get; set; }
        public string? Adress
        {
            get; set;
        }
        public int CourseId { get; set; }
        public  Course Course { get; set; } 
        public int? InstractorId { get; set; }
        public  Instractor? Instractor { get; set; }
    }
}

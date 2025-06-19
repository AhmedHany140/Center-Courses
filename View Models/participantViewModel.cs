using CenterDragon.Models.Entites;
using System.ComponentModel.DataAnnotations;

namespace CenterDragon.View_Models
{
    public class participantViewModel
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

        [Display(Name = "Course")]
        public int ParCourseId { get; set; }

        public List<Course> Courselist { get; set; } = new List<Course>();
    }

}

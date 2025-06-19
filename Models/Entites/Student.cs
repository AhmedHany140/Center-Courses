using System.ComponentModel.DataAnnotations;

namespace CenterDragon.Models.Entites
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string password { get; set; }
        public string SecurtyKey { get; set; }

        public int? Age { get; set; }
        public string? Ediation { get; set; }
        public string? Adress
        {
            get; set;
        }


        public virtual List<Message> MassagesSend { get; set; } = new List<Message>();
        public virtual List<Message> MassagesRecieved { get; set; } = new List<Message>();
    }



}

using System.ComponentModel.DataAnnotations;

namespace CenterDragon.Models.Entites
{
    public class Admin
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string password { get; set; }
        public string SecurtyKey { get; set; }
        public string? About { get; set; }
    }
}

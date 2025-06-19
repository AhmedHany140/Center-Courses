using System.ComponentModel.DataAnnotations;

namespace CenterDragon.Models.Entites
{
    public class Secertary
    {
        public int Id { get; set; }
        public string FullName { get; set; }
  
        public string SecurtyKey { get; set; }

        public string? About {  get; set; }
        public virtual List<Message> MassagesSend { get; set; } = new List<Message>();
        public virtual List<Message> MassagesRecieved { get; set; } = new List<Message>();
    }
}

namespace CenterDragon.Models.Entites
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public bool IsReplyed { get; set; } = false;
        public string? SenderName { get; set; }

        public int? StudentSenderId { get; set; }
        public Student? StudentSender { get; set; }

        public int? SecertarySenderId { get; set; }
        public Secertary? SecertarySender { get; set; }

        public int? StudentReciverId { get; set; }
        public Student? StudentReciver { get; set; }

        public int? SecertaryReciverId { get; set; }
        public Secertary? SecertaryReciver { get; set; }
    }

}

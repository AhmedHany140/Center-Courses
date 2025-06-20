using CenterDragon.Models.Entites;

namespace CenterDragon.Interfaces
{
	public interface IMessageRepository
	{
		Task AddMessageAsync(Message message);
		Task<List<Message>> GetMessagesForStudentAsync(string studentSecurityKey);
		Task<List<Message>> GetMessagesForSecretaryAsync(string secretarySecurityKey);
		Task UpdateMessageAsync(Message message);
	}
}

using CenterDragon.Data;
using CenterDragon.Interfaces;
using CenterDragon.Models.Entites;
using Microsoft.EntityFrameworkCore;

namespace CenterDragon.Reposatories
{
	/// <summary>
	/// Repository for managing message-related data operations.
	/// </summary>
	public class MessageRepository : IMessageRepository
	{
		private readonly ApplicationDbContext _context;

		/// <summary>
		/// Initializes a new instance of the <see cref="MessageRepository"/> class.
		/// </summary>
		/// <param name="context">The application database context.</param>
		public MessageRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Adds a new message to the database asynchronously.
		/// </summary>
		/// <param name="message">The message entity to add.</param>
		public async Task AddMessageAsync(Message message)
		{
			await _context.Messages.AddAsync(message);
			await _context.SaveChangesAsync();
		}

		/// <summary>
		/// Retrieves all messages for a specific student by their security key.
		/// </summary>
		/// <param name="studentSecurityKey">The security key of the student.</param>
		/// <returns>A list of messages for the student.</returns>
		public async Task<List<Message>> GetMessagesForStudentAsync(string studentSecurityKey)
		{
			return await _context.Messages.AsNoTracking()
				.Include(x => x.SecertarySender)
				.Include(x => x.StudentReciver)
				.Where(x => x.StudentReciver.SecurtyKey == studentSecurityKey && !x.IsReplyed)
				.ToListAsync();
		}

		/// <summary>
		/// Retrieves all messages for a specific secretary by their security key.
		/// </summary>
		/// <param name="secretarySecurityKey">The security key of the secretary.</param>
		/// <returns>A list of messages for the secretary.</returns>
		public async Task<List<Message>> GetMessagesForSecretaryAsync(string secretarySecurityKey)
		{
			return await _context.Messages.AsNoTracking()
				.Include(x => x.StudentSender)
				.Include(x => x.SecertaryReciver)
				.Where(x => x.SecertaryReciver.SecurtyKey == secretarySecurityKey && !x.IsReplyed)
				.ToListAsync();
		}

		/// <summary>
		/// Updates an existing message in the database asynchronously.
		/// </summary>
		/// <param name="message">The message entity to update.</param>
		public async Task UpdateMessageAsync(Message message)
		{
			_context.Messages.Update(message);
			await _context.SaveChangesAsync();
		}
	}
}

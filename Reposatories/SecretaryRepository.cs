using CenterDragon.Data;
using CenterDragon.Interfaces;
using CenterDragon.Models.Entites;
using Microsoft.EntityFrameworkCore;

namespace CenterDragon.Reposatories
{
	/// <summary>
	/// Repository for managing secretary-related data operations.
	/// </summary>
	public class SecretaryRepository : ISecretaryRepository
	{
		private readonly ApplicationDbContext _context;

		/// <summary>
		/// Initializes a new instance of the <see cref="SecretaryRepository"/> class.
		/// </summary>
		/// <param name="context">The application database context.</param>
		public SecretaryRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Retrieves the first secretary from the database asynchronously.
		/// </summary>
		/// <returns>The first <see cref="Secertary"/> entity, or null if not found.</returns>
		public async Task<Secertary> GetFirstSecretaryAsync()
		{
			return await _context.Secretaries.FirstOrDefaultAsync();
		}

		/// <summary>
		/// Retrieves a secretary by their security key asynchronously.
		/// </summary>
		/// <param name="securityKey">The security key of the secretary.</param>
		/// <returns>The <see cref="Secertary"/> entity matching the security key, or null if not found.</returns>
		public async Task<Secertary> GetSecretaryBySecurityKeyAsync(string securityKey)
		{
			return await _context.Secretaries.FirstOrDefaultAsync(x => x.SecurtyKey == securityKey);
		}
	}
}

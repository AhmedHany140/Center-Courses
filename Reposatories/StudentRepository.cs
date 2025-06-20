using CenterDragon.Data;
using CenterDragon.Interfaces;
using CenterDragon.Models.Entites;
using Microsoft.EntityFrameworkCore;

namespace CenterDragon.Reposatories
{
	/// <summary>
	/// Repository for managing student-related data operations.
	/// </summary>
	public class StudentRepository : IStudentRepository
	{
		private readonly ApplicationDbContext _context;

		/// <summary>
		/// Initializes a new instance of the <see cref="StudentRepository"/> class.
		/// </summary>
		/// <param name="context">The application database context.</param>
		public StudentRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Retrieves a student by their security key asynchronously.
		/// </summary>
		/// <param name="securityKey">The security key of the student.</param>
		/// <returns>The student entity if found; otherwise, null.</returns>
		public async Task<Student> GetStudentBySecurityKeyAsync(string securityKey)
		{
			return await _context.Students.FirstOrDefaultAsync(x => x.SecurtyKey == securityKey);
		}

		/// <summary>
		/// Adds a new student to the database asynchronously.
		/// </summary>
		/// <param name="student">The student entity to add.</param>
		public async Task AddStudentAsync(Student student)
		{
			await _context.Students.AddAsync(student);
			await _context.SaveChangesAsync();
		}
	}
}

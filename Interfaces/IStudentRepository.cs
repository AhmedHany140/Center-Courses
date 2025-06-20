using CenterDragon.Models.Entites;

namespace CenterDragon.Interfaces
{
	public interface IStudentRepository
	{
		Task<Student> GetStudentBySecurityKeyAsync(string securityKey);
		Task AddStudentAsync(Student student);
	}
}

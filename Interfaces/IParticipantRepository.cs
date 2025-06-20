using CenterDragon.Models.Entites;

namespace CenterDragon.Interfaces
{
	public interface IParticipantRepository
	{
		List<Participants>? GetAll();
		void DeletebyId(int id);
		void Edit(Participants val);
		void Add(Participants val);
		Participants? GetById(int id);
		void Save();
		int? GetInstractorId(int Courseid);
		List<Course>? AllCourses();

		List<Student>? AllStudents();

	}
}

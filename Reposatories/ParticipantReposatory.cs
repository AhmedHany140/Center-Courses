using CenterDragon.Data;
using CenterDragon.Interfaces;
using CenterDragon.Models.Entites;
using Microsoft.EntityFrameworkCore;

namespace CenterDragon.Reposatories
{
	/// <summary>
	/// Repository for managing participant-related data operations.
	/// </summary>
	public class ParticipantReposatory : IParticipantRepository
	{
		private readonly ApplicationDbContext Context;

		/// <summary>
		/// Initializes a new instance of the <see cref="ParticipantReposatory"/> class.
		/// </summary>
		/// <param name="_context">The application database context.</param>
		public ParticipantReposatory(ApplicationDbContext _context)
		{
			Context = _context;
		}

		/// <summary>
		/// Adds a new participant to the context.
		/// </summary>
		/// <param name="val">The participant to add.</param>
		public void Add(Participants val)
		{
			Context.participants.Add(val);
		}

		/// <summary>
		/// Retrieves all courses.
		/// </summary>
		/// <returns>List of courses.</returns>
		public List<Course>? AllCourses()
		{
			return Context.Courses.ToList();
		}

		/// <summary>
		/// Retrieves all students.
		/// </summary>
		/// <returns>List of students.</returns>
		public List<Student>? AllStudents()
		{
			return Context.Students.ToList();
		}

		/// <summary>
		/// Deletes a participant by their ID.
		/// </summary>
		/// <param name="id">The participant's ID.</param>
		public void DeletebyId(int id)
		{
			var p = Context.participants.FirstOrDefault(x => x.Id == id);
			if (p != null)
			{
				Context.participants.Remove(p);
			}
		}

		/// <summary>
		/// Edits an existing participant's details.
		/// </summary>
		/// <param name="val">The participant with updated values.</param>
		public void Edit(Participants val)
		{
			var nativepart = Context.participants.FirstOrDefault(x => x.Id == val.Id);
			nativepart.Adress = val.Adress;
			nativepart.Email = val.Email;
			nativepart.Ediation = val.Ediation;
			nativepart.CourseId = val.CourseId;
			nativepart.FullName = val.FullName;
			nativepart.Age = val.Age;
		}

		/// <summary>
		/// Retrieves all participants with their instructors and courses.
		/// </summary>
		/// <returns>List of participants.</returns>
		public List<Participants>? GetAll()
		{
			return Context.participants
				.Include(x => x.Instractor)
				.Include(x => x.Course).ToList();
		}

		/// <summary>
		/// Retrieves a participant by their ID, including instructor and course.
		/// </summary>
		/// <param name="id">The participant's ID.</param>
		/// <returns>The participant, or null if not found.</returns>
		public Participants? GetById(int id)
		{
			return Context.participants
				.Include(x => x.Instractor)
				.Include(x => x.Course)
				.FirstOrDefault(x => x.Id == id);
		}

		/// <summary>
		/// Gets the instructor ID for a given course.
		/// </summary>
		/// <param name="Courseid">The course ID.</param>
		/// <returns>The instructor ID, or null if not found.</returns>
		public int? GetInstractorId(int Courseid)
		{
			Course c = Context.Courses.FirstOrDefault(x => x.Id == Courseid);
			return c.instractorId;
		}

		/// <summary>
		/// Saves all changes made in the context to the database.
		/// </summary>
		public void Save()
		{
			Context.SaveChanges();
		}
	}
}

using CenterDragon.Models.Entites;
using CenterDragon.View_Models;

namespace CenterDragon.Interfaces
{
    public interface IStudent
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

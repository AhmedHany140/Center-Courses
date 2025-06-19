using CenterDragon.Data;
using CenterDragon.Interfaces;
using CenterDragon.Models.Entites;
using CenterDragon.View_Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.UI.V5.Pages.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace CenterDragon.Reposatories
{
    public class ParticipantReposatory : IStudent
    {
        private readonly ApplicationDbContext Context;
        public ParticipantReposatory(ApplicationDbContext _context)
        {
            Context = _context;
        }

        public void Add(Participants val)
        {
          Context.participants.Add(val);
        }

        public  List<Course>? AllCourses()
        {
         return  Context.Courses.ToList();
        }

        public  List<Student>?AllStudents()
        {
            return  Context.Students.ToList();
        }

        public void DeletebyId(int id)
        {
            var p = Context.participants.FirstOrDefault(x => x.Id == id);
            if(p != null)
            {
                 Context.participants.Remove(p);
            }

        }

        public void Edit(Participants val)
        {
           var nativepart= Context.participants.FirstOrDefault(x=>x.Id==val.Id);
            nativepart.Adress=val.Adress;
            nativepart.Email=val.Email;
            nativepart.Ediation=val.Ediation;
            nativepart.CourseId=val.CourseId;
            nativepart.FullName=val.FullName;
            nativepart.Age=val.Age;

        }

        public  List<Participants>? GetAll()
        {
           return   Context.participants
                .Include(x=>x.Instractor)
                .Include(x=>x.Course).ToList();
        }

        public Participants? GetById(int id)
        {


            return  Context.participants
                .Include(x=>x.Instractor)
                .Include(x => x.Course)
                .FirstOrDefault(x => x.Id == id);


        }

        public  int? GetInstractorId(int Courseid)
        {
            Course c = Context.Courses.FirstOrDefault(x => x.Id == Courseid);
         
            return  c.instractorId;
        }

        public void Save()
        {
           Context.SaveChanges();
        }

     
    }
}

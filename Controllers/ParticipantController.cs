using CenterDragon.Data;
using CenterDragon.Interfaces;
using CenterDragon.Models.Entites;
using CenterDragon.Reposatories;
using CenterDragon.View_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;

namespace CenterDragon.Controllers
{
    public class ParticipantController : Controller
    {
        private readonly IStudent participantReposatory;
        private readonly UserManager<ApplicationUser> userManager;

        public ParticipantController(IStudent _ParticipantReposatory, UserManager<ApplicationUser> userManager)
        {
            participantReposatory = _ParticipantReposatory;
            this.userManager = userManager;
        }
        public async Task< IActionResult> Index()
        {
            var partis= participantReposatory.GetAll();
            return View("Index", partis);
        }
        public async Task< IActionResult> Add()
        {
            var partis = new Participants();
            var courses=new List< Course>();

            var partcourse = new participantViewModel();

            partcourse.Id = partis.Id;
            partcourse.Adress=partis.Adress;
            partcourse.Age= partis.Age;
            partcourse.Email=partis.Email;
            partcourse.FullName=partis.FullName;
            partcourse.Ediation=partis.Ediation;
            partcourse.ParCourseId=partis.CourseId;
            if(participantReposatory.AllCourses()  != null) 
            partcourse.Courselist = participantReposatory.AllCourses();

            return View("Add", partcourse);
        }

        public async Task<IActionResult> SaveData(participantViewModel P)
        {
            if (ModelState.IsValid)
            {
                if (P.ParCourseId != 0)
                {
                    Participants partis = new Participants();
                    int? instId =  participantReposatory.GetInstractorId(P.ParCourseId);
                    partis.Adress = P.Adress;
                    partis.Age = P.Age;
                    partis.Email = P.Email;
                    partis.FullName = P.FullName;
                    partis.Ediation = P.Ediation;
                    partis.CourseId = P.ParCourseId;
                    if (instId.HasValue)
                        partis.InstractorId = instId.Value;

                     participantReposatory.Add(partis);
                     participantReposatory.Save();

                  
                    List<Student> students =  participantReposatory.AllStudents();
                    string SKey = null;
                    foreach (var item in students)
                    {
                        if (item.Email == partis.Email)
                            SKey = item.SecurtyKey;
                    }

                    if (SKey != null)
                    {
                        var ParUser = await userManager.FindByIdAsync(SKey);
                        if (ParUser != null)
                        {
                            switch(partis.CourseId)
                            {
                                    case 1:
                                    var roleResult1 = await userManager.AddToRoleAsync(ParUser, "BackEnd");
                                    if (!roleResult1.Succeeded)
                                    {
                                       
                                        foreach (var error in roleResult1.Errors)
                                        {
                                            ModelState.AddModelError(string.Empty, error.Description);
                                        }
                                    }
                                    break;
                                    case 2:
                                    var roleResult2 = await userManager.AddToRoleAsync(ParUser, "FrontEnd");
                                    if (!roleResult2.Succeeded)
                                    {
                                      
                                        foreach (var error in roleResult2.Errors)
                                        {
                                            ModelState.AddModelError(string.Empty, error.Description);
                                        }
                                    }
                                    break;
                                    case 3:
                                    var roleResult3 = await userManager.AddToRoleAsync(ParUser, "FullStack");
                                    if (!roleResult3.Succeeded)
                                    {
                                       
                                        foreach (var error in roleResult3.Errors)
                                        {
                                            ModelState.AddModelError(string.Empty, error.Description);
                                        }
                                    }
                                    break;
                            }
                               
                          
               

                
                        }
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    P.Courselist =  participantReposatory.AllCourses();
                    ModelState.AddModelError("CourseId", "Choose a Course");
                }
            }

            return View("Add", P);
        }


        public async Task< IActionResult> Delete(int id)
        {
            participantReposatory.DeletebyId(id);
            participantReposatory.Save();
            return RedirectToAction("Index");

        }
        public async Task< IActionResult> Details(int id)
        {
           var p=  participantReposatory.GetById(id);
            return View(p);
        }
        public async Task<IActionResult> Edit(int  id)
        {
            var partis = participantReposatory.GetById(id);
        
            var courses = new List<Course>();

            var partcourse = new participantViewModel();

            partcourse.Id = partis.Id;
            partcourse.Adress = partis.Adress;
            partcourse.Age = partis.Age;
            partcourse.Email = partis.Email;
            partcourse.FullName = partis.FullName;
            partcourse.Ediation = partis.Ediation;
            partcourse.ParCourseId = partis.CourseId;
            if (participantReposatory.AllCourses() != null)
                partcourse.Courselist = participantReposatory.AllCourses();

            return View(partcourse);
        }

        public async Task<IActionResult> SaveEdite(participantViewModel participantView)
        {
            if (ModelState.IsValid)
            {
                if (participantView.ParCourseId != 0)
                {
                    Participants partis = new Participants();
                     partis.Id = participantView.Id;
                    partis.Adress = participantView.Adress;
                    partis.Age = participantView.Age;
                    partis.Email = participantView.Email;
                    partis.FullName = participantView.FullName;
                    partis.Ediation = participantView.Ediation;
                    partis.CourseId = participantView.ParCourseId;
                  
                     participantReposatory.Edit(partis);
                     participantReposatory.Save();


                    return RedirectToAction("Index");
                }
                else
                {
                  
                    ModelState.AddModelError("CourseId", "Choose a Course");
                }
            }
            participantView.Courselist = participantReposatory.AllCourses();
            return View("Edit", participantView);
        }




   


    }
}

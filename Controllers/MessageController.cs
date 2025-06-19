using CenterDragon.Data;
using CenterDragon.Models.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using System.Collections;

namespace CenterDragon.Controllers
{
    public class MessageController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private ApplicationUser currentuser { get; set; }
        private Dictionary<string, List<string>> messagelist { get; set; }
        public MessageController(ApplicationDbContext context,UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
            messagelist = new Dictionary<string, List<string>>();
        }

        public async Task< IActionResult> Index(string RecieverType, string? Contentvalue, string? coursename)
        {
            ViewBag.RecieverType = RecieverType;
            ViewBag.Contentvalue = Contentvalue;
            ViewBag.coursename = coursename;
            return View();
        }
        public async Task<IActionResult>
            AddMessage(Message message, string? RecieverType,string? Contentvalue,string? coursename)
        {
            if (RecieverType == "Secertary" && RecieverType is not null)
            {
                currentuser = await userManager.GetUserAsync(User);
                var exest = await context.Students
            .FirstOrDefaultAsync(x => x.SecurtyKey == currentuser.Id);
                if (exest == null)
                {

                    var student = new Student()
                    {
                        FullName = currentuser.FullName,
                        SecurtyKey = currentuser.Id,
                        Email = currentuser.Email,
                        password = currentuser.PasswordHash,
                        Age = currentuser.Age,
                        Ediation = currentuser.Ediation,
                        Adress = currentuser.Adress

                    };

                    await context.Students.AddAsync(student);
                    await context.SaveChangesAsync();
                }

        
                var reciver = await context.Secretaries.FirstOrDefaultAsync();

                var sender = await context.Students.
                    FirstOrDefaultAsync(x => x.SecurtyKey == currentuser.Id);
                 

                await context.Messages.AddAsync(new Message
                {
                    StudentSender = sender,
                    Content = $"{message.Content} \n [course : {coursename}]",
                    SecertaryReciver = reciver,
                    Date = DateTime.Now,
                    SenderName = sender.FullName,
                    IsReplyed = false,

                });
               await context.SaveChangesAsync();

                return RedirectToAction("Index", "Home"); 

            }
            else
            {
                currentuser = await userManager.GetUserAsync(User);
              
               var reciever1=await context.Students.FirstOrDefaultAsync(x=>x.FullName== RecieverType);

                var reciever2 = await context.Secretaries.FirstOrDefaultAsync(x => x.FullName == RecieverType);
                if (reciever1 != null)
                {
                    var sender = await context.Secretaries
                        .FirstOrDefaultAsync(x => x.SecurtyKey == currentuser.Id);
                    await context.Messages.AddAsync(new Message
                    {
                        SecertarySender = sender,
                        Content = $"{message.Content} \n [course : {coursename}]",
                        StudentReciver = reciever1,
                        Date = DateTime.Now,
                        SenderName = sender.FullName,
                       

                    });

                    var mess =await context.Messages.Where(x => x.SecertaryReciver == sender)
                        .ToListAsync();
                    foreach (var item in mess)
                    {
                        if(item.Content==Contentvalue)
                            item.IsReplyed = true;
                    }
                   await context.SaveChangesAsync();
                    return RedirectToAction("Index", "Secrtary"); 
                }
                else if(reciever2 != null)
                {
                    var sender = await context.Students
                      .FirstOrDefaultAsync(x => x.SecurtyKey == currentuser.Id);
                    await context.Messages.AddAsync(new Message
                    {
                        StudentSender = sender,
                        Content = $"{message.Content} \n [course : {coursename}]",
                        SecertaryReciver = reciever2,
                        Date = DateTime.Now,
                        SenderName = sender.FullName,
                      

                    });

                    var mess = await context.Messages.Where(x => x.StudentReciver == sender)
                   .ToListAsync();
                    foreach (var item in mess)
                    {
                        if (item.Content == Contentvalue)
                            item.IsReplyed = true;
                    }
                    await context.SaveChangesAsync();
                    return RedirectToAction("Index", "Home"); 
                }



                return NotFound();
            }
           
           



        }

        public async Task<IActionResult> ShowMessages(string? kind)
        {
            currentuser = await userManager.GetUserAsync(User);
            var userid = currentuser.Id;

            var messagelist = new Dictionary<string, List<string>>();

            if (kind.ToLower() == "student")
            {
                var messages = await context.Messages.AsNoTracking()
                    .Include(x => x.SecertarySender)  
                    .Include(x => x.StudentReciver)   
                    .Where(x => x.StudentReciver.SecurtyKey == userid && !x.IsReplyed)  
                    .ToListAsync();

                foreach (var m in messages)
                {
                    if (m.StudentReciver != null && m.SecertarySender != null)
                    {
                        if (messagelist.ContainsKey(m.SenderName))
                        {
                            messagelist[m.SenderName].Add(m.Content);
                        }
                        else
                        {
                            messagelist[m.SenderName] = new List<string> { m.Content };
                        }
                    }
                }
            }
            else if (kind.ToLower() == "secertary")
            {
                var messages = await context.Messages.AsNoTracking()
                    .Include(x => x.StudentSender)   
                    .Include(x => x.SecertaryReciver) 
                    .Where(x => x.SecertaryReciver.SecurtyKey == userid && !x.IsReplyed) 
                    .ToListAsync();

                var stdSender = messages.Select(m => m.StudentSender).FirstOrDefault();

                 ViewBag.StdSender = stdSender;

                foreach (var m in messages)
                {
                    if (m.SecertaryReciver != null && m.StudentSender != null)
                    {
                        if (messagelist.ContainsKey(m.SenderName))
                        {
                            messagelist[m.SenderName].Add(m.Content);
                        }
                        else
                        {
                            messagelist[m.SenderName] = new List<string> { m.Content };
                        }
                    }
                }
            }

            ViewBag.allmessages = messagelist; 
            return View();
        }


    }
}

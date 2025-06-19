using CenterDragon.Models.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CenterDragon.View_Models;
using System.Reflection.Emit;

namespace CenterDragon.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Instractor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Secertary> Secretaries { get; set; }
        public DbSet<Participants> participants { get; set; }
        public DbSet<Message> Messages { get; set; }




        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

       
   
            builder.Entity<Student>().ToTable("Students");
            builder.Entity<Instractor>().ToTable("Instructors");
            builder.Entity<Admin>().ToTable("Admins");
            builder.Entity<Secertary>().ToTable("Secretaries");

           
            builder.Entity<Message>()
                .HasOne(m => m.StudentSender)
                .WithMany(s => s.MassagesSend)  
                .HasForeignKey(m => m.StudentSenderId)
                .OnDelete(DeleteBehavior.Restrict);

          
            builder.Entity<Message>()
                .HasOne(m => m.SecertarySender)
                .WithMany(s => s.MassagesSend)  
                .HasForeignKey(m => m.SecertarySenderId)
                .OnDelete(DeleteBehavior.Restrict);

           
            builder.Entity<Message>()
                .HasOne(m => m.StudentReciver)
                .WithMany(s => s.MassagesRecieved)  
                .HasForeignKey(m => m.StudentReciverId)
                .OnDelete(DeleteBehavior.Restrict);

          
            builder.Entity<Message>()
                .HasOne(m => m.SecertaryReciver)
                .WithMany(s => s.MassagesRecieved) 
                .HasForeignKey(m => m.SecertaryReciverId)
                .OnDelete(DeleteBehavior.Restrict);
        }

     
    }
}

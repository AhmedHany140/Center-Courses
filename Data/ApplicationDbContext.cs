using CenterDragon.Models.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CenterDragon.View_Models;
using System.Reflection.Emit;

namespace CenterDragon.Data
{
	/// <summary>
	/// The Entity Framework database context for the application, including Identity and domain entities.
	/// </summary>
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
		/// </summary>
		/// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		/// <summary>
		/// Gets or sets the students in the system.
		/// </summary>
		public DbSet<Student> Students { get; set; }

		/// <summary>
		/// Gets or sets the instructors in the system.
		/// </summary>
		public DbSet<Instractor> Instructors { get; set; }

		/// <summary>
		/// Gets or sets the courses in the system.
		/// </summary>
		public DbSet<Course> Courses { get; set; }

		/// <summary>
		/// Gets or sets the admins in the system.
		/// </summary>
		public DbSet<Admin> Admins { get; set; }

		/// <summary>
		/// Gets or sets the secretaries in the system.
		/// </summary>
		public DbSet<Secertary> Secretaries { get; set; }

		/// <summary>
		/// Gets or sets the participants in the system.
		/// </summary>
		public DbSet<Participants> participants { get; set; }

		/// <summary>
		/// Gets or sets the messages in the system.
		/// </summary>
		public DbSet<Message> Messages { get; set; }

		/// <summary>
		/// Configures the schema needed for the application and relationships between entities.
		/// </summary>
		/// <param name="builder">The model builder being used to construct the model for this context.</param>
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

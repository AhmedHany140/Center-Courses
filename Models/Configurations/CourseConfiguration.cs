using CenterDragon.Models.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CenterDragon.Models.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(40);

            builder.HasOne(x => x.instractor)
                 .WithOne(x => x.Course)
                 .HasForeignKey<Course>(x => x.instractorId)
                 .OnDelete(DeleteBehavior.NoAction);

        }
    }
}

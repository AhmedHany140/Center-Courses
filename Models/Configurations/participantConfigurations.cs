using CenterDragon.Models.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CenterDragon.Models.Configurations
{
    public class participantConfigurations : IEntityTypeConfiguration<Participants>
    {
        public void Configure(EntityTypeBuilder<Participants> builder)
        {
           builder.HasKey(x => x.Id);
           
           builder.HasOne(x=>x.Course)
                .WithMany(x=>x.Participants)
                .HasForeignKey(x=>x.CourseId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Instractor)
              .WithMany(x => x.Participants)
              .HasForeignKey(x => x.InstractorId)
              .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

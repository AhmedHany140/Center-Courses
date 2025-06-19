using CenterDragon.Models.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CenterDragon.Models.Configurations
{
    public class InstractorConfiguration : IEntityTypeConfiguration<Instractor>
    {
        public void Configure(EntityTypeBuilder<Instractor> builder)
        {
           builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(40);

           
               
        }
    }
}

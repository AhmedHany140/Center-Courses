using CenterDragon.Models.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CenterDragon.Models.Configurations
{
    public class SecertaryConfiguration : IEntityTypeConfiguration<Secertary>
    {
        public void Configure(EntityTypeBuilder<Secertary> builder)
        {
          builder.HasKey(e => e.Id);
            builder.Property(x => x.About).HasMaxLength(150);
        }
    }
}

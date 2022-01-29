using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities;

namespace Infrastructure.Configurations
{
    public class AreaUnitConfiguration : IEntityTypeConfiguration<AreaUnit>
    {
        public void Configure(EntityTypeBuilder<AreaUnit> builder)
        {
            builder.ToTable("AreaUnit", "Lookup");

            builder.HasKey(p => p.AreaUnitId)
                .HasName("PK_AreaUnit");

            builder.Property(p => p.AreaUnitId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .HasColumnType("VARCHAR(250)")
                .IsRequired();

        }
    }
}

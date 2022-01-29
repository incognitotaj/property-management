using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities;

namespace Infrastructure.Configurations
{
    public class PurposeConfiguration : IEntityTypeConfiguration<Purpose>
    {
        public void Configure(EntityTypeBuilder<Purpose> builder)
        {
            builder.ToTable("Purpose", "Lookup");

            builder.HasKey(p => p.PurposeId)
                .HasName("PK_Purpose");

            builder.Property(p => p.PurposeId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .HasColumnType("VARCHAR(250)")
                .IsRequired();

        }
    }
}

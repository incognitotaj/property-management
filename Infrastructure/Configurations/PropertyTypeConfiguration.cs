using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities;

namespace Infrastructure.Configurations
{
    public class PropertyTypeConfiguration : IEntityTypeConfiguration<PropertyType>
    {
        public void Configure(EntityTypeBuilder<PropertyType> builder)
        {
            builder.ToTable("PropertyType", "Lookup");

            builder.HasKey(p => p.PropertyTypeId)
                .HasName("PK_PropertyType");

            builder.Property(p => p.PropertyTypeId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .HasColumnType("VARCHAR(250)")
                .IsRequired();

        }
    }
}

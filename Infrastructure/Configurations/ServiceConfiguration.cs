using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities;

namespace Infrastructure.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("Service", "Property");

            builder.Property(p => p.ServiceId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .HasColumnType("VARCHAR(50)")
                .IsRequired();

            builder.Property(p => p.Description)
                .HasColumnType("VARCHAR(2000)")
                .IsRequired();

            builder.Property(p => p.IsActive)
                .HasColumnType("BIT");

            builder.Property(p => p.Photo)
                .HasColumnType("VARBINARY(MAX)")
                .IsRequired();

            builder.Property(p => p.Created)
                .HasColumnType("DATETIME")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

        }
    }
}

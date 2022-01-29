using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities;

namespace Infrastructure.Configurations
{
    public class PropertyRequestConfiguration : IEntityTypeConfiguration<PropertyRequest>
    {
        public void Configure(EntityTypeBuilder<PropertyRequest> builder)
        {
            builder.ToTable("PropertyRequest", "Property");

            builder.Property(p => p.PropertyRequestId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.FirstName)
                .HasColumnType("VARCHAR(150)")
                .IsRequired();

            builder.Property(p => p.LastName)
                .HasColumnType("VARCHAR(150)")
                .IsRequired(false);
            
            builder.Property(p => p.Mobile)
                .HasColumnType("VARCHAR(25)")
                .IsRequired();

            builder.Property(p => p.Email)
                .HasColumnType("VARCHAR(350)")
                .IsRequired();

            builder.Property(p => p.Message)
                .HasColumnType("VARCHAR(2000)")
                .IsRequired();

            builder.Property(p => p.PropertyId)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.IsRead)
                .HasColumnType("BIT");

            builder.Property(p => p.Created)
                .HasColumnType("DATETIME")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(p => p.Property)
                .WithMany(p => p.PropertyRequests)
                .HasForeignKey(p => p.PropertyId)
                .HasConstraintName("FK_PropertyRequest_Property")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

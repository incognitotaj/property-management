using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities;

namespace Infrastructure.Configurations
{
    public class ServiceRequestConfiguration : IEntityTypeConfiguration<ServiceRequest>
    {
        public void Configure(EntityTypeBuilder<ServiceRequest> builder)
        {
            builder.ToTable("ServiceRequest", "Property");

            builder.Property(p => p.ServiceRequestId)
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

            builder.Property(p => p.ServiceId)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.IsRead)
                .HasColumnType("BIT");

            builder.Property(p => p.Created)
                .HasColumnType("DATETIME")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(p => p.Service)
                .WithMany(p => p.ServiceRequests)
                .HasForeignKey(p => p.ServiceId)
                .HasConstraintName("FK_ServiceRequest_Service")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

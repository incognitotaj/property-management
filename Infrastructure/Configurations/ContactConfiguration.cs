using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities;

namespace Infrastructure.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("Contact", "Property");

            builder.HasKey(p => p.ContactId)
                .HasName("PK_Contact");

            builder.Property(p => p.ContactId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.FirstName)
                .HasColumnType("VARCHAR(250)")
                .IsRequired();

            builder.Property(p => p.LastName)
                .HasColumnType("VARCHAR(250)")
                .IsRequired(false);

            builder.Property(p => p.Email)
                .HasColumnType("VARCHAR(350)")
                .IsRequired();

            builder.Property(p => p.Mobile)
                .HasColumnType("VARCHAR(25)")
                .IsRequired();

            builder.Property(p => p.Message)
                .HasColumnType("VARCHAR(2000)")
                .IsRequired();

            builder.Property(p => p.IsActive)
                .HasColumnType("BIT");

            builder.Property(p => p.Created)
                .HasColumnType("DATETIME")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");
        }
    }
}

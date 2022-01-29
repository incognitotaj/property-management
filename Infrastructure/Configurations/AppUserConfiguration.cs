using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities;

namespace Infrastructure.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("User", "Identity");

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.UserName)
                .HasColumnType("VARCHAR(350)")
                .IsRequired();

            builder.Property(p => p.Email)
                .HasColumnType("VARCHAR(350)")
                .IsRequired();

            builder.Property(p => p.PhoneNumber)
                .HasColumnType("VARCHAR(25)");

            builder.Property(p => p.FirstName)
                .HasColumnType("VARCHAR(50)")
                .IsRequired();

            builder.Property(p => p.LastName)
                .HasColumnType("VARCHAR(50)")
                .IsRequired(false);

            builder.Property(p => p.Created)
                .HasColumnType("DATETIME")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

        }
    }
}

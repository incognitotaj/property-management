using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities;

namespace Infrastructure.Configurations
{
    public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.ToTable("UserLogin", "Identity");

            builder.HasKey(u => new { u.LoginProvider, u.ProviderKey });

            builder.HasOne(ur => ur.User)
                .WithMany(ur => ur.UserLogins)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        }
    }
}

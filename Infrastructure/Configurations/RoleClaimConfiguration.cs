using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
    {
        public void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            builder.ToTable("RoleClaim", "Identity");

            builder.HasKey(rc => rc.Id);

            builder.HasOne(ur => ur.Role)
                .WithMany(ur => ur.RoleClaims)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
        }
    }
}

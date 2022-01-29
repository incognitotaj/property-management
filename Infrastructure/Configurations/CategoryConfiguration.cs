using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities;

namespace Infrastructure.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category", "Lookup");

            builder.HasKey(p => p.CategoryId)
                .HasName("PK_Category");

            builder.Property(p => p.CategoryId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .HasColumnType("VARCHAR(250)")
                .IsRequired();

        }
    }
}

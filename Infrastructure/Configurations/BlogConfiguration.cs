using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities;

namespace Infrastructure.Configurations
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.ToTable("Blog", "Property");

            builder.HasKey(p => p.BlogId)
                .HasName("PK_Blog");

            builder.Property(p => p.BlogId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Title)
                .HasColumnType("VARCHAR(250)")
                .IsRequired();

            builder.Property(p => p.CategoryId)
                .HasColumnType("INT")
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

            builder.HasOne(p => p.Category)
                .WithMany(p => p.Blogs)
                .HasForeignKey(p => p.CategoryId)
                .HasConstraintName("FK_Blog_Category")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}

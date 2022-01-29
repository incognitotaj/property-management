using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities;

namespace Infrastructure.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comment", "Property");

            builder.HasKey(p => p.CommentId)
                .HasName("PK_Comment");

            builder.Property(p => p.CommentId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.BlogId)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.Name)
                .HasColumnType("VARCHAR(250)")
                .IsRequired();

            builder.Property(p => p.Email)
                .HasColumnType("VARCHAR(350)")
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

            builder.HasOne(p => p.Blog)
                .WithMany(p => p.Comments)
                .HasForeignKey(p => p.BlogId)
                .HasConstraintName("FK_Comment_Blog")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}

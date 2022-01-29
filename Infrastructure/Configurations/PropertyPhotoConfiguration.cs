using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities;

namespace Infrastructure.Configurations
{
    public class PropertyPhotoConfiguration : IEntityTypeConfiguration<PropertyPhoto>
    {
        public void Configure(EntityTypeBuilder<PropertyPhoto> builder)
        {
            builder.ToTable("PropertyPhoto", "Property");

            builder.HasKey(p => p.PropertyPhotoId)
                .HasName("PK_PropertyPhoto");

            builder.Property(p => p.PropertyPhotoId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.PropertyId)
                .HasColumnType("INT")
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

            builder.HasOne(p => p.Property)
                .WithMany(p => p.PropertyPhotos)
                .HasForeignKey(p => p.PropertyId)
                .HasConstraintName("FK_PropertyPhoto_Property")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}

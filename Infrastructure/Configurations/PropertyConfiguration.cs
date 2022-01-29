using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities;

namespace Infrastructure.Configurations
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.ToTable("Property", "Property");

            builder.HasKey(p => p.PropertyId)
                .HasName("PK_Property");

            builder.Property(p => p.PropertyId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Title)
                .HasColumnType("VARCHAR(250)")
                .IsRequired();

            builder.Property(p => p.Description)
                .HasColumnType("VARCHAR(2000)")
                .IsRequired();

            builder.Property(p => p.PropertyTypeId)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.PurposeId)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.CityId)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.AreaUnitId)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(p => p.Address)
                 .HasColumnType("VARCHAR(2000)")
                 .IsRequired();

            builder.Property(p => p.Price)
                .HasColumnType("NUMERIC(18, 2)")
                .IsRequired();

            builder.Property(p => p.LandArea)
                .HasColumnType("NUMERIC(18, 2)")
                .IsRequired();

            builder.Property(p => p.Name)
                .HasColumnType("VARCHAR(250)")
                .IsRequired();

            builder.Property(p => p.Email)
                .HasColumnType("VARCHAR(350)")
                .IsRequired();
            
            builder.Property(p => p.Mobile)
                .HasColumnType("VARCHAR(25)")
                .IsRequired();

            builder.Property(p => p.IsActive)
                .HasColumnType("BIT");

            builder.Property(p => p.IsFeatured)
                .HasColumnType("BIT");

            builder.Property(p => p.IsSold)
                .HasColumnType("BIT");

            builder.Property(p => p.Created)
                .HasColumnType("DATETIME")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(p => p.Photo)
                .HasColumnType("VARBINARY(MAX)")
                .IsRequired();

            builder.Property(p => p.BedRooms)
                .HasColumnType("INT")
                .HasDefaultValueSql("0")
                .IsRequired();

            builder.Property(p => p.BathRooms)
                .HasColumnType("INT")
                .HasDefaultValueSql("0")
                .IsRequired();

            builder.Property(p => p.Floors)
                .HasColumnType("INT")
                .HasDefaultValueSql("0")
                .IsRequired();

            builder.Property(p => p.Kitchens)
                .HasColumnType("INT")
                .HasDefaultValueSql("0")
                .IsRequired();

            builder.HasOne(p => p.PropertyType)
                .WithMany(p => p.Properties)
                .HasForeignKey(p => p.PropertyTypeId)
                .HasConstraintName("FK_Property_PropertyType")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Purpose)
                .WithMany(p => p.Properties)
                .HasForeignKey(p => p.PurposeId)
                .HasConstraintName("FK_Property_Purpose")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.City)
                .WithMany(p => p.Properties)
                .HasForeignKey(p => p.CityId)
                .HasConstraintName("FK_Property_City")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.AreaUnit)
                .WithMany(p => p.Properties)
                .HasForeignKey(p => p.AreaUnitId)
                .HasConstraintName("FK_Property_AreaUnit")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

using Entities;
using Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure
{
    // ORM -> Object <--> Relation Mapper
    // ORM -> C# Object <--> SQL Table Mapper
    public class AskDbContext : IdentityDbContext<AppUser, AppRole, string,
        UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        private readonly IConfigurationRoot _config;

        public AskDbContext(DbContextOptions<AskDbContext> options,
            IConfigurationRoot config)
            : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:AskConnectionString"]);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ASP.NET Core Identity
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new UserLoginConfiguration());
            modelBuilder.ApplyConfiguration(new UserClaimConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserTokenConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.ApplyConfiguration(new RoleClaimConfiguration());

            // Property
            modelBuilder.ApplyConfiguration(new ServiceConfiguration());
            modelBuilder.ApplyConfiguration(new BlogConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyRequestConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyPhotoConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceRequestConfiguration());
            modelBuilder.ApplyConfiguration(new ContactConfiguration());

            // Lookups
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new AreaUnitConfiguration());
            modelBuilder.ApplyConfiguration(new CityConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PurposeConfiguration());
        }

        public DbSet<Service> Services { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<AreaUnit> AreaUnits { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<Purpose> Purposes { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyPhoto> PropertyPhotos { get; set; }
        public DbSet<PropertyRequest> PropertyRequests { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }

}

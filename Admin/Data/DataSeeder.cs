using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Entities;
using Infrastructure;

namespace Admin.Data
{
    public class DataSeeder
    {
        private readonly AskDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public DataSeeder(AskDbContext context,
            IWebHostEnvironment env,
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager)
        {
            _context = context;
            _env = env;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            _context.Database.EnsureCreated();

            if (!_userManager.Users.Any())
            {
                var path = Path.Combine(_env.ContentRootPath, "Data/SeedData/users-data.json");
                var json = File.ReadAllText(path);
                var temp = JsonConvert.DeserializeObject<IEnumerable<AppUser>>(json);
                foreach (var user in temp)
                {
                    await _userManager.CreateAsync(new AppUser
                    {
                        Created = DateTime.Now,
                        Email = user.Email,
                        UserName = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                    }, "password");
                }
            }

            if (!_roleManager.Roles.Any())
            {
                var path = Path.Combine(_env.ContentRootPath, "Data/SeedData/roles-data.json");
                var json = File.ReadAllText(path);
                var temp = JsonConvert.DeserializeObject<IEnumerable<AppRole>>(json);
                foreach (var role in temp)
                {
                    await _roleManager.CreateAsync(new AppRole
                    {
                        Created = DateTime.Now,
                        Name = role.Name,
                    });
                }
            }

            _context.SaveChanges();
        }
    }
}

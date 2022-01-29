using Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IEFRepository _repository;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(IEFRepository repository,
            UserManager<AppUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet()]
        public async Task<IActionResult> Summary()
        {
            var services = await _repository.GetServicesAsync();
            var blogs = await _repository.GetBlogsAsync(null);
            var properties = await _repository.GetPropertiesAsync();
            var users = await _userManager.Users.ToListAsync();

            return Json(new
            {
                services = services.Count(),
                blogs = blogs.Count(),
                properties = properties.Count(),
                users = users.Count()
            });
        }
    }
}

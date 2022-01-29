using Admin.ViewModels;
using Entities;
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
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UsersController(UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        #region Read
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet()]
        public IActionResult LoadData(int isActive, int? pageNumber, int? pageSize, string textSearch)
        {
            return ViewComponent("Admin.ViewComponents.UsersList", 
                new { isActive, pageNumber, pageSize, textSearch });
        }
        #endregion

        #region Create
        [HttpGet()]
        public IActionResult Create()
        {
            var model = new UserCreateViewModel();
            return View(model);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser()
                {
                    Created = DateTime.Now,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Email,
                    PhoneNumber = model.Mobile,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Error while saving data");
                }
            }
            else
            {
                ModelState.AddModelError("", "Provide all required data to proceed");
            }

            return PartialView(model);
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(p => p.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new UserUpdateViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                Mobile = user.PhoneNumber,
            };

            return PartialView(model);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UserUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(p => p.Id == model.Id);
                if (user == null)
                {
                    return NotFound();
                }

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.PhoneNumber = model.Mobile;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Error while saving data");
                }
            }
            else
            {
                ModelState.AddModelError("", "Provide all required data to proceed");
            }

            return PartialView(model);
        }
        #endregion
    }
}

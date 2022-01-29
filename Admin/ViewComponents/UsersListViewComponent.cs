using Admin.Helpers;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.ViewComponents
{
    public class UsersListViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public UsersListViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int isActive, int? pageNumber, int? pageSize, string textSearch)
        {
            var items = await GetItemsAsync(isActive, pageNumber, pageSize, textSearch);
            return View(items);
        }
        private async Task<PaginatedList<AppUser>> GetItemsAsync(int isActive, int? pageNumber, int? pageSize, string textSearch)
        {
            var result = await _userManager.Users.ToListAsync();

            result = result.ToList();

            if (!string.IsNullOrEmpty(textSearch) && !string.IsNullOrWhiteSpace(textSearch))
            {
                result = result.Where(p => p.UserName.ToLower().Contains(textSearch.ToLower())).ToList();
            }

            var pagedList = PaginatedList<AppUser>.Create(result.AsQueryable(), pageNumber ?? 1, pageSize ?? 5);

            return pagedList;
        }
    }
}

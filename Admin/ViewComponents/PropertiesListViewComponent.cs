using Admin.Helpers;
using Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.ViewComponents
{
    public class PropertiesListViewComponent : ViewComponent
    {
        private readonly IEFRepository _repository;

        public PropertiesListViewComponent(IEFRepository repository)
        {
            _repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int isActive, int? pageNumber, int? pageSize, string textSearch)
        {
            var items = await GetItemsAsync(isActive, pageNumber, pageSize, textSearch);
            return View(items);
        }
        private async Task<PaginatedList<Property>> GetItemsAsync(int isActive, int? pageNumber, int? pageSize, string textSearch)
        {
            var result = await _repository.GetPropertiesAsync(true);
            if (isActive != 3)
            {
                result = result.Where(p => p.IsActive == (isActive == 1) ? true : false);
            }

            if (!string.IsNullOrEmpty(textSearch) && !string.IsNullOrWhiteSpace(textSearch))
            {
                result = result.Where(p => p.Title.ToLower().Contains(textSearch.ToLower()));
            }

            var pagedList = PaginatedList<Property>.Create(result.AsQueryable(), pageNumber ?? 1, pageSize ?? 5);

            return pagedList;
        }
    }
}

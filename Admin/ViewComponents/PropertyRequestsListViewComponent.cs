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
    public class PropertyRequestsListViewComponent : ViewComponent
    {
        private readonly IEFRepository _repository;

        public PropertyRequestsListViewComponent(IEFRepository repository)
        {
            _repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int propertyId, int isRead, int? pageNumber, int? pageSize, string textSearch)
        {
            var items = await GetItemsAsync(propertyId, isRead, pageNumber, pageSize, textSearch);
            return View(items);
        }
        private async Task<PaginatedList<PropertyRequest>> GetItemsAsync(int propertyId, int isRead, int? pageNumber, int? pageSize, string textSearch)
        {
            var result = await _repository.GetPropertyRequestsAsync(propertyId, true);
            if (isRead != 3)
            {
                result = result.Where(p => p.IsRead == (isRead == 1) ? true : false);
            }

            if (!string.IsNullOrEmpty(textSearch) && !string.IsNullOrWhiteSpace(textSearch))
            {
                result = result.Where(p => p.FirstName.ToLower().Contains(textSearch.ToLower()));
            }

            var pagedList = PaginatedList<PropertyRequest>.Create(result.AsQueryable(), pageNumber ?? 1, pageSize ?? 5);

            return pagedList;
        }
    }
}

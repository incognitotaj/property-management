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
    public class ServiceRequestsListViewComponent : ViewComponent
    {
        private readonly IEFRepository _repository;

        public ServiceRequestsListViewComponent(IEFRepository repository)
        {
            _repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int serviceId, int isRead, int? pageNumber, int? pageSize, string textSearch)
        {
            var items = await GetItemsAsync(serviceId, isRead, pageNumber, pageSize, textSearch);
            return View(items);
        }
        private async Task<PaginatedList<ServiceRequest>> GetItemsAsync(int serviceId, int isRead, int? pageNumber, int? pageSize, string textSearch)
        {
            var result = await _repository.GetServiceRequestsAsync(serviceId, true);
            if (isRead != 3)
            {
                result = result.Where(p => p.IsRead == (isRead == 1) ? true : false);
            }

            if (!string.IsNullOrEmpty(textSearch) && !string.IsNullOrWhiteSpace(textSearch))
            {
                result = result.Where(p => p.FirstName.ToLower().Contains(textSearch.ToLower()));
            }

            var pagedList = PaginatedList<ServiceRequest>.Create(result.AsQueryable(), pageNumber ?? 1, pageSize ?? 5);

            return pagedList;
        }
    }
}

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
    public class ContactsListViewComponent : ViewComponent
    {
        private readonly IEFRepository _repository;

        public ContactsListViewComponent(IEFRepository repository)
        {
            _repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int isActive, int? pageNumber, int? pageSize, string textSearch)
        {
            var items = await GetItemsAsync(isActive, pageNumber, pageSize, textSearch);
            return View(items);
        }
        private async Task<PaginatedList<Contact>> GetItemsAsync(int isActive, int? pageNumber, int? pageSize, string textSearch)
        {
            var result = await _repository.GetContactsAsync(true);
            if (isActive != 3)
            {
                result = result.Where(p => p.IsActive == (isActive == 1) ? true : false);
            }

            if (!string.IsNullOrEmpty(textSearch) && !string.IsNullOrWhiteSpace(textSearch))
            {
                result = result.Where(p => p.FirstName.ToLower().Contains(textSearch.ToLower()) ||
                                           p.LastName.ToLower().Contains(textSearch.ToLower()) ||
                                           p.Email.ToLower().Contains(textSearch.ToLower()));
            }

            var pagedList = PaginatedList<Contact>.Create(result.AsQueryable(), pageNumber ?? 1, pageSize ?? 5);

            return pagedList;
        }
    }
}

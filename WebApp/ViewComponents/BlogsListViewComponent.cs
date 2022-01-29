using Entities;
using Entities.Enumerations;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewComponents
{
    public class BlogsListViewComponent : ViewComponent
    {
        private readonly IEFRepository _repository;

        public BlogsListViewComponent(IEFRepository repository)
        {
            _repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await GetItemsAsync();

            return View(items);
        }
        private async Task<IEnumerable<Blog>> GetItemsAsync()
        {
            var result = await _repository.GetBlogsAsync(null, true);
            return result.Where(p => p.IsActive == true);
        }
    }
}

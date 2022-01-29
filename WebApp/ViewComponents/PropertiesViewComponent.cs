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
    public class PropertiesViewComponent : ViewComponent
    {
        private readonly IEFRepository _repository;

        public PropertiesViewComponent(IEFRepository repository)
        {
            _repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int purposeId)
        {
            var items = await GetItemsAsync(purposeId);

            if (purposeId == (int)PropertyPurpose.Rent)
            {
                return View("PropertyForRent", items);
            }

            return View("PropertyForSale", items);
        }
        private async Task<IEnumerable<Property>> GetItemsAsync(int purposeId)
        {
            var result = await _repository.GetPropertiesAsync(true);
            return result.Where(p => p.PurposeId == purposeId && p.IsActive == true);
        }
    }
}

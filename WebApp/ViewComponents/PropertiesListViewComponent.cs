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
    public class PropertiesListViewComponent : ViewComponent
    {
        private readonly IEFRepository _repository;

        public PropertiesListViewComponent(IEFRepository repository)
        {
            _repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync(
            string title, int cityId, int propertyTypeId, int purposeId,
            int beds, int baths, int areaFrom, int areaTo,
            int priceFrom, int priceTo)
        {
            var items = await GetItemsAsync(title, cityId, propertyTypeId, purposeId,
             beds, baths, areaFrom, areaTo,
             priceFrom, priceTo);
            return View(items);
        }
        private async Task<IEnumerable<Property>> GetItemsAsync(
            string title, int cityId, int propertyTypeId, int purposeId,
            int beds, int baths, int areaFrom, int areaTo,
            int priceFrom, int priceTo)
        {
            var result = await _repository.GetPropertiesAsync(true);

            if (!string.IsNullOrEmpty(title) && !string.IsNullOrWhiteSpace(title))
            {
                result = result.Where(p => p.Title.ToLower().Contains(title.ToLower()));
            }

            if (cityId != 0)
            {
                result = result.Where(p => p.CityId == cityId);
            }

            if (propertyTypeId != 0)
            {
                result = result.Where(p => p.PropertyTypeId == propertyTypeId);
            }

            if (purposeId != 0)
            {
                result = result.Where(p => p.PurposeId == purposeId);
            }

            if (beds != 0)
            {
                result = result.Where(p => p.BedRooms == beds);
            }

            if (baths != 0)
            {
                result = result.Where(p => p.BathRooms == baths);
            }

            if (areaTo != 0)
            {
                result = result.Where(p => p.LandArea >= areaFrom && p.LandArea <= areaTo);
            }
            else if (areaTo == 0)
            {
                result = result.Where(p => p.LandArea >= areaFrom);
            }


            if (priceTo != 0)
            {
                result = result.Where(p => p.Price >= priceFrom && p.Price <= priceTo);
            }
            else if (priceTo == 0)
            {
                result = result.Where(p => p.Price >= priceFrom);
            }

            return result.Where(p => p.IsActive == true);
        }
    }
}

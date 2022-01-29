using Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly IEFRepository _repository;

        public PropertiesController(IEFRepository repository)
        {
            _repository = repository;
        }

        #region Read

        
        public async Task<IActionResult> Index()
        {
            var cities = await _repository.GetCitiesAsync();
            var propertyTypes = await _repository.GetPropertyTypesAsync();
            var purposes = await _repository.GetPurposesAsync();
            var areaUnits = await _repository.GetAreaUnitsAsync();

            var model = new PropertySearchViewModel
            {
                AreaUnits = areaUnits.Select(p => new SelectListItem { Text = p.Name, Value = p.AreaUnitId.ToString() }),
                Cities = cities.Select(p => new SelectListItem { Text = p.Name, Value = p.CityId.ToString() }),
                PropertyTypes = propertyTypes.Select(p => new SelectListItem { Text = p.Name, Value = p.PropertyTypeId.ToString() }),
                Purposes = purposes.Select(p => new SelectListItem { Text = p.Name, Value = p.PurposeId.ToString() }),
            };

            return View(model);
        }

        [HttpGet()]
        public IActionResult Listing(
            string title, int cityId, int propertyTypeId, int purposeId,
            int beds, int baths, int areaFrom, int areaTo,
            int priceFrom, int priceTo)
        {
            return ViewComponent("WebApp.ViewComponents.PropertiesList",
                new
                {
                    title,
                    cityId,
                    propertyTypeId,
                    purposeId,
                    beds,
                    baths,
                    areaFrom,
                    areaTo,
                    priceFrom,
                    priceTo
                });
        }
        #endregion

        #region Create
        [HttpGet()]
        public async Task<IActionResult> Create()
        {
            var cities = await _repository.GetCitiesAsync();
            var propertyTypes = await _repository.GetPropertyTypesAsync();
            var purposes = await _repository.GetPurposesAsync();
            var areaUnits = await _repository.GetAreaUnitsAsync();
            var model = new PropertyCreateViewModel
            {
                AreaUnits = areaUnits.Select(p => new SelectListItem { Text = p.Name, Value = p.AreaUnitId.ToString() }),
                Cities = cities.Select(p => new SelectListItem { Text = p.Name, Value = p.CityId.ToString() }),
                PropertyTypes = propertyTypes.Select(p => new SelectListItem { Text = p.Name, Value = p.PropertyTypeId.ToString() }),
                Purposes = purposes.Select(p => new SelectListItem { Text = p.Name, Value = p.PurposeId.ToString() }),
            };

            return View(model);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PropertyCreateViewModel model, IFormFile Photo)
        {
            if (ModelState.IsValid)
            {
                if (Photo != null && Photo.Length > 0)
                {
                    //Getting FileName
                    var fileName = Path.GetFileName(Photo.FileName);
                    //Getting file Extension
                    var fileExtension = Path.GetExtension(fileName);
                    // concatenating  FileName + FileExtension
                    var newFileName = string.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);
                }
                var property = new Property()
                {
                    Created = DateTime.Now,
                    Name = model.Name,
                    Description = model.Description,
                    IsActive = false,
                    Address = model.Address,
                    AreaUnitId = model.AreaUnitId,
                    CityId = model.CityId,
                    Email = model.Email,
                    LandArea = model.LandArea,
                    Mobile = model.Mobile,
                    Price = model.Price,
                    PropertyTypeId = model.PropertyTypeId,
                    PurposeId = model.PurposeId,
                    Title = model.Title,
                    IsFeatured = false,
                    BedRooms = model.BedRooms,
                    BathRooms = model.BathRooms,
                    Kitchens = model.Kitchens,
                    Floors = model.Floors,
                    IsSold = model.IsSold
                };

                using (var target = new MemoryStream())
                {
                    await Photo.CopyToAsync(target);
                    property.Photo = target.ToArray();
                }

                await _repository.AddAsync(property);
                if (!await _repository.SaveChangesAsync())
                {
                    ModelState.AddModelError("", "Error while saving data");
                }
                else
                {
                    return RedirectToAction("Index", new { area = "", controller = "Properties" });
                }
            }
            else
            {
                ModelState.AddModelError("", "Provide all required data to proceed");
            }

            var cities = await _repository.GetCitiesAsync();
            var propertyTypes = await _repository.GetPropertyTypesAsync();
            var purposes = await _repository.GetPurposesAsync();
            var areaUnits = await _repository.GetAreaUnitsAsync();

            model.AreaUnits = areaUnits.Select(p => new SelectListItem { Text = p.Name, Value = p.AreaUnitId.ToString() });
            model.Cities = cities.Select(p => new SelectListItem { Text = p.Name, Value = p.CityId.ToString() });
            model.PropertyTypes = propertyTypes.Select(p => new SelectListItem { Text = p.Name, Value = p.PropertyTypeId.ToString() });
            model.Purposes = purposes.Select(p => new SelectListItem { Text = p.Name, Value = p.PurposeId.ToString() });

            return View(model);
        }
        #endregion
    }
}

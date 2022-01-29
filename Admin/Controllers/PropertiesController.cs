using Admin.ViewModels;
using Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Controllers
{
    [Authorize]
    public class PropertiesController : Controller
    {
        private readonly IEFRepository _repository;

        public PropertiesController(IEFRepository repository)
        {
            _repository = repository;
        }

        #region Read
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet()]
        public IActionResult LoadData(int isActive, int? pageNumber, int? pageSize, string textSearch)
        {
            return ViewComponent("Admin.ViewComponents.PropertiesList", 
                new { isActive, pageNumber, pageSize, textSearch });
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
                    IsActive = model.IsActive,
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
                    IsFeatured = model.IsFeatured,
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

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int propertyId)
        {
            var property = await _repository.GetPropertyByIdAsync(propertyId);
            if (property == null)
            {
                return NotFound();
            }

            var cities = await _repository.GetCitiesAsync();
            var propertyTypes = await _repository.GetPropertyTypesAsync();
            var purposes = await _repository.GetPurposesAsync();
            var areaUnits = await _repository.GetAreaUnitsAsync();

            var model = new PropertyUpdateViewModel
            {
                Name = property.Name,
                Description = property.Description,
                PropertyId = property.PropertyId,
                CurrentPhoto = property.Photo,
                IsActive = property.IsActive,
                Address = property.Address,
                AreaUnitId = property.AreaUnitId,
                CityId = property.CityId,
                Email = property.Email,
                LandArea = property.LandArea,
                Mobile = property.Mobile,
                Price = property.Price,
                PropertyTypeId = property.PropertyTypeId,
                PurposeId = property.PurposeId,
                Title = property.Title,
                IsFeatured = property.IsFeatured,
                IsSold = property.IsSold,
                BedRooms = property.BedRooms,
                BathRooms = property.BathRooms,
                Floors = property.Floors,
                Kitchens = property.Kitchens,
                AreaUnits = areaUnits.Select(p => new SelectListItem { Text = p.Name, Value = p.AreaUnitId.ToString() }),
                Cities = cities.Select(p => new SelectListItem { Text = p.Name, Value = p.CityId.ToString() }),
                PropertyTypes = propertyTypes.Select(p => new SelectListItem { Text = p.Name, Value = p.PropertyTypeId.ToString() }),
                Purposes = purposes.Select(p => new SelectListItem { Text = p.Name, Value = p.PurposeId.ToString() }),
            };

            return PartialView(model);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(PropertyUpdateViewModel model, IFormFile Photo)
        {
            if (ModelState.IsValid)
            {
                var property = await _repository.GetPropertyByIdAsync(model.PropertyId);
                if (property == null)
                {
                    return NotFound();
                }

                property.Name = model.Name;
                property.Description = model.Description;
                property.IsActive = model.IsActive;
                property.Address = model.Address;
                property.AreaUnitId = model.AreaUnitId;
                property.CityId = model.CityId;
                property.Email = model.Email;
                property.LandArea = model.LandArea;
                property.Mobile = model.Mobile;
                property.Price = model.Price;
                property.PropertyTypeId = model.PropertyTypeId;
                property.PurposeId = model.PurposeId;
                property.Title = model.Title;
                property.IsActive = model.IsActive;
                property.IsFeatured = model.IsFeatured;
                property.BedRooms = model.BedRooms;
                property.BathRooms = model.BathRooms;
                property.Floors = model.Floors;
                property.Kitchens = model.Kitchens;
                property.IsSold = model.IsSold;

                if (Photo != null && Photo.Length > 0)
                {
                    using (var target = new MemoryStream())
                    {
                        await Photo.CopyToAsync(target);
                        property.Photo = target.ToArray();
                    }
                }

                await _repository.UpdateAsync(property);
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

            return PartialView(model);
        }
        #endregion

        #region Toggle Status
        [HttpPost()]
        public async Task<IActionResult> Toggle(int id)
        {
            bool Status = false;
            string Message = string.Empty;

            var temp = await _repository.GetPropertyByIdAsync(id);
            if (temp == null)
            {
                return NotFound();
            }

            temp.IsActive = temp.IsActive == true ? false : true;
            await _repository.UpdateAsync(temp);
            if (await _repository.SaveChangesAsync())
            {
                Status = true;
                Message = "Record updated successfully";
            }
            else
            {
                Status = false;
                Message = "Error while updating record";
            }

            return Json(new { status = Status, message = Message });
        }
        #endregion
    }
}

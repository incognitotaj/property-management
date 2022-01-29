using Admin.ViewModels;
using Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Controllers
{
    [Authorize]
    public class ServicesController : Controller
    {
        private readonly IEFRepository _repository;

        public ServicesController(IEFRepository repository)
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
            return ViewComponent("Admin.ViewComponents.ServicesList", new { isActive, pageNumber, pageSize, textSearch });
        }
        #endregion

        #region Create
        [HttpGet()]
        public IActionResult Create()
        {
            var model = new ServiceCreateViewModel();
            return View(model);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceCreateViewModel model, IFormFile Photo)
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
                var service = new Service()
                {
                    Created = DateTime.Now,
                    Name = model.Name,
                    Description = model.Description,
                    IsActive = model.IsActive
                };

                using (var target = new MemoryStream())
                {
                    await Photo.CopyToAsync(target);
                    service.Photo = target.ToArray();
                }

                await _repository.AddAsync(service);
                if (!await _repository.SaveChangesAsync())
                {
                    ModelState.AddModelError("", "Error while saving data");
                }
            }
            else
            {
                ModelState.AddModelError("", "Provide all required data to proceed");
            }

            return PartialView(model);
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var service = await _repository.GetServiceByIdAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            var model = new ServiceUpdateViewModel
            {
                Name = service.Name,
                Description = service.Description,
                ServiceId = service.ServiceId,
                CurrentPhoto = service.Photo,
                IsActive = service.IsActive
            };

            return PartialView(model);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ServiceUpdateViewModel model, IFormFile Photo)
        {
            if (ModelState.IsValid)
            {
                var service = await _repository.GetServiceByIdAsync(model.ServiceId);
                if (service == null)
                {
                    return NotFound();
                }

                service.Name = model.Name;
                service.Description = model.Description;
                service.IsActive = model.IsActive;

                if (Photo != null && Photo.Length > 0)
                {
                    using (var target = new MemoryStream())
                    {
                        await Photo.CopyToAsync(target);
                        service.Photo = target.ToArray();
                    }
                }

                await _repository.UpdateAsync(service);
                if (!await _repository.SaveChangesAsync())
                {
                    ModelState.AddModelError("", "Error while saving data");
                }
            }
            else
            {
                ModelState.AddModelError("", "Provide all required data to proceed");
            }

            return PartialView(model);
        }
        #endregion

        #region Toggle Status
        [HttpPost()]
        public async Task<IActionResult> Toggle(int id)
        {
            bool Status = false;
            string Message = string.Empty;

            var temp = await _repository.GetServiceByIdAsync(id);
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

using Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IEFRepository _repository;

        public ServicesController(IEFRepository repository)
        {
            _repository = repository;
        }

        [HttpGet()]
        public async Task<IActionResult> Index()
        {
            var services = await _repository.GetServicesAsync(true);
            var model = new ServiceRequestCreateViewModel
            {
                Services = services
                            .Where(p=>p.IsActive == true)
                            .Select(p=> new SelectListItem { Text = p.Name, Value = p.ServiceId.ToString()  })
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ServiceRequestCreateViewModel model)
        {
            bool Status = true;
            string Message = string.Empty;

            var services = await _repository.GetServicesAsync();

            model.Services = services
                .Where(p => p.IsActive == true)
                .Select(p => new SelectListItem { Text = p.Name, Value = p.ServiceId.ToString() });

            if (ModelState.IsValid)
            {
                var serviceRequest = new ServiceRequest
                {
                    Created = DateTime.Now,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    IsRead = false,
                    LastName = model.LastName,
                    Message = model.Message,
                    Mobile = model.Mobile,
                    ServiceId = model.ServiceId,
                };

                await _repository.AddAsync(serviceRequest);
                if (await _repository.SaveChangesAsync())
                {
                    Status = true;
                    Message = "Service request sent successfully";
                }
                else
                {
                    Status = false;
                    Message = "Error has occured";
                }
            }
            else
            {
                Status = false;
                Message = "Provide all valid details to proceed";
            }

            return Json(new { status = Status, message = Message });
        }

        [HttpGet()]
        public IActionResult Message()
        {
            return PartialView();
        }
    }
}

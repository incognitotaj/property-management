using Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class PropertyRequestsController : Controller
    {
        private readonly IEFRepository _repository;

        public PropertyRequestsController(IEFRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Create
        [HttpGet()]
        public IActionResult Create(int propertyId)
        {
            var model = new PropertyRequestCreateViewModel()
            {
                PropertyId = propertyId

            };
            return View(model);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PropertyRequestCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var propertyRequest = new PropertyRequest()
                {
                    PropertyId = model.PropertyId,
                    Created = DateTime.Now,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Message = model.Message,
                    Mobile = model.Mobile,
                    IsRead = false
                };

                await _repository.AddAsync(propertyRequest);
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

        [HttpGet()]
        public IActionResult Message()
        {
            return PartialView();
        }
    }
}

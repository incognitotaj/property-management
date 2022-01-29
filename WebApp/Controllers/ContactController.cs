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
    public class ContactController : Controller
    {
        private readonly IEFRepository _repository;

        public ContactController(IEFRepository repository)
        {
            _repository = repository;
        }

        [HttpGet()]
        public IActionResult Index()
        {
            var model = new ContactCreateViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ContactCreateViewModel model)
        {
            bool Status = true;
            string Message = string.Empty;
            if (ModelState.IsValid)
            {
                var contact = new Contact
                {
                    Created = DateTime.Now,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    IsActive = true,
                    Message = model.Message,
                    Mobile = model.Mobile,
                };

                await _repository.AddAsync(contact);
                if (await _repository.SaveChangesAsync())
                {
                    Status = true;
                    Message = "Contact request sent successfully";
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

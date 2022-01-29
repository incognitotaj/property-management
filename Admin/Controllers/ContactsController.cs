using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Controllers
{
    [Authorize]
    public class ContactsController : Controller
    {
        private readonly IEFRepository _repository;

        public ContactsController(IEFRepository repository)
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
            return ViewComponent("Admin.ViewComponents.ContactsList", 
                new { isActive, pageNumber, pageSize, textSearch });
        }
        #endregion

        [HttpGet()]
        public async Task<IActionResult> Message(int contactId)
        {
            var temp = await _repository.GetContactByIdAsync(contactId);
            if (temp == null)
            {
                return NotFound();
            }

            return PartialView(temp);
        }

        #region Toggle Status
        [HttpPost()]
        public async Task<IActionResult> Toggle(int id)
        {
            bool Status = false;
            string Message = string.Empty;

            var temp = await _repository.GetContactByIdAsync(id);
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

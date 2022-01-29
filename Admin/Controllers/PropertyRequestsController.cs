using Admin.ViewModels;
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
    public class PropertyRequestsController : Controller
    {
        private readonly IEFRepository _repository;

        public PropertyRequestsController(IEFRepository repository)
        {
            _repository = repository;
        }

        #region Read
        [HttpGet]
        public IActionResult Index(int propertyId)
        {
            var model = new PropertyRequestIndexViewModel
            {
                PropertyId = propertyId
            };

            return View(model);
        }

        [HttpGet()]
        public IActionResult LoadData(int propertyId, int isRead, int? pageNumber, int? pageSize, string textSearch)
        {
            return ViewComponent("Admin.ViewComponents.PropertyRequestsList", new { propertyId, isRead, pageNumber, pageSize, textSearch });
        }
        #endregion

        #region Toggle Status
        [HttpPost()]
        public async Task<IActionResult> Toggle(int id)
        {
            bool Status = false;
            string Message = string.Empty;

            var temp = await _repository.GetPropertyRequestByIdAsync(id);
            if (temp == null)
            {
                return NotFound();
            }

            temp.IsRead = temp.IsRead == true ? false : true;
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

        [HttpGet]
        public async Task<IActionResult> Message(int id)
        {
            var temp = await _repository.GetPropertyRequestByIdAsync(id);
            if (temp == null)
            {
                return NotFound();
            }
            return View(temp);
        }
    }
}

using Admin.ViewModels;
using Entities;
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
    public class CommentsController : Controller
    {
        private readonly IEFRepository _repository;

        public CommentsController(IEFRepository repository)
        {
            _repository = repository;
        }

        #region Read
        [HttpGet()]
        public IActionResult LoadData(int blogid, int isActive, int? pageNumber, int? pageSize, string textSearch)
        {
            return ViewComponent("Admin.ViewComponents.CommentsList", 
                new { blogid, isActive, pageNumber, pageSize, textSearch });
        }
        #endregion

        #region Create
        [HttpGet()]
        public IActionResult Create(int blogId)
        {
            var model = new CommentCreateViewModel()
            {
                BlogId = blogId,

            };
            return View(model);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CommentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var comment = new Comment()
                {
                    BlogId = model.BlogId,
                    Created = DateTime.Now,
                    Name = model.Name,
                    Email = model.Email,
                    Message = model.Message,
                    IsActive = model.IsActive
                };

                await _repository.AddAsync(comment);
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
            var comment = await _repository.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            var model = new CommentUpdateViewModel
            {
                Name = comment.Name,
                Email = comment.Email,
                CommentId = comment.CommentId,
                Message = comment.Message,
                IsActive = comment.IsActive
            };

            return PartialView(model);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CommentUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var comment = await _repository.GetCommentByIdAsync(model.CommentId);
                if (comment == null)
                {
                    return NotFound();
                }

                comment.Name = model.Name;
                comment.Message = model.Message;
                comment.Email = model.Email;
                comment.IsActive = model.IsActive;

                await _repository.UpdateAsync(comment);
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

            var temp = await _repository.GetCommentByIdAsync(id);
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

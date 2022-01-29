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
    public class BlogsController : Controller
    {
        private readonly IEFRepository _repository;

        public BlogsController(IEFRepository repository)
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
            return ViewComponent("Admin.ViewComponents.BlogsList", new { isActive, pageNumber, pageSize, textSearch });
        }

        [HttpGet()]
        public async Task<IActionResult> Details(int blogid)
        {
            var blog = await _repository.GetBlogByIdAsync(blogid, true);

            return View(blog);
        }
        #endregion

        #region Create
        [HttpGet()]
        public async Task<IActionResult> Create()
        {
            var categories = await _repository.GetCategoriesAsync();
            var model = new BlogCreateViewModel()
            {
                Categories = categories.Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.CategoryId.ToString()
                })
            };
            return View(model);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCreateViewModel model, IFormFile Photo)
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
                var service = new Blog()
                {
                    Created = DateTime.Now,
                    Title = model.Title,
                    Description = model.Description,
                    CategoryId = model.CategoryId,
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

            var categories = await _repository.GetCategoriesAsync();

            model.Categories = categories.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.CategoryId.ToString()
            });

            return PartialView(model);
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var blog = await _repository.GetBlogByIdAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            var categories = await _repository.GetCategoriesAsync();

            var model = new BlogUpdateViewModel
            {
                Title = blog.Title,
                Description = blog.Description,
                CategoryId = blog.CategoryId,
                BlogId = blog.BlogId,
                CurrentPhoto = blog.Photo,
                Categories = categories.Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.CategoryId.ToString()
                }),
                IsActive = blog.IsActive
            };

            return PartialView(model);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(BlogUpdateViewModel model, IFormFile Photo)
        {
            if (ModelState.IsValid)
            {
                var blog = await _repository.GetBlogByIdAsync(model.BlogId);
                if (blog == null)
                {
                    return NotFound();
                }

                blog.Title = model.Title;
                blog.Description = model.Description;
                blog.CategoryId = model.CategoryId;
                blog.IsActive = model.IsActive;

                if (Photo != null && Photo.Length > 0)
                {
                    using (var target = new MemoryStream())
                    {
                        await Photo.CopyToAsync(target);
                        blog.Photo = target.ToArray();
                    }
                }

                await _repository.UpdateAsync(blog);
                if (!await _repository.SaveChangesAsync())
                {
                    ModelState.AddModelError("", "Error while saving data");
                }
            }
            else
            {
                ModelState.AddModelError("", "Provide all required data to proceed");
            }

            var categories = await _repository.GetCategoriesAsync();

            model.Categories = categories.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.CategoryId.ToString()
            });

            return PartialView(model);
        }
        #endregion

        #region Toggle Status
        [HttpPost()]
        public async Task<IActionResult> Toggle(int id)
        {
            bool Status = false;
            string Message = string.Empty;

            var temp = await _repository.GetBlogByIdAsync(id);
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

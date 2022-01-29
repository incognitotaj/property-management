using Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.ViewComponents;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IEFRepository _repository;

        public BlogsController(IEFRepository repository)
        {
            _repository = repository;
        }

        [HttpGet()]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int blogId)
        {
            var blog = await _repository.GetBlogByIdAsync(blogId, true);
            var model = new CommentCreateViewModel
            {
                Blog = blog,
                BlogId = blog.BlogId
            };
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(CommentCreateViewModel model)
        {
            var blog = await _repository.GetBlogByIdAsync(model.BlogId, true);
            model.Blog = blog;
            model.BlogId = blog.BlogId;

            bool Status = true;
            string Message = string.Empty;

            if (ModelState.IsValid)
            {
                var comment = new Comment
                {
                    Created = DateTime.Now,
                    Email = model.Email,
                    Name = model.Name,
                    IsActive = false,
                    Message = model.Message,
                    BlogId = model.BlogId,
                };

                await _repository.AddAsync(comment);
                if (await _repository.SaveChangesAsync())
                {
                    Status = true;
                    Message = "Comment posted successfully";
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

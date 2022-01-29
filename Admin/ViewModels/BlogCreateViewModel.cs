using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Admin.ViewModels
{
    public class BlogCreateViewModel
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(250, ErrorMessage = "{0} must be {2} - {1} characters", MinimumLength = 3)]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(2000, ErrorMessage = "{0} must be {2} - {1} characters", MinimumLength = 3)]
        public string Description { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "{0} is required")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }

        public bool IsActive { get; set; }
    }
}

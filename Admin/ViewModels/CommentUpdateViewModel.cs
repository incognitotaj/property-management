using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Admin.ViewModels
{
    public class CommentUpdateViewModel
    {
        [Required]
        public int CommentId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(250, ErrorMessage = "{0} must be {2} - {1} characters", MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(250, ErrorMessage = "{0} must be max of {1} characters")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Message")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(2000, ErrorMessage = "{0} must be {2} - {1} characters", MinimumLength = 3)]
        public string Message { get; set; }

        public bool IsActive { get; set; }
    }
}

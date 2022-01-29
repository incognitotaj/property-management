using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class ContactCreateViewModel
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(250, ErrorMessage = "{0} must be {2} - {1} characters", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(250, ErrorMessage = "{0} must net exceeds {1} characters")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(250, ErrorMessage = "{0} must be max of {1} characters")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Mobile")]
        [Required(ErrorMessage = "{0} is required")]
        public string Mobile { get; set; }

        [Display(Name = "Message")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(2000, ErrorMessage = "{0} must be {2} - {1} characters", MinimumLength = 3)]
        public string Message { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.ViewModels
{
    public class UserCreateViewModel
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(250, ErrorMessage = "{0} must be {2} - {1} characters", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(250, ErrorMessage = "{0} must be {2} - {1} characters", MinimumLength = 3)]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} is required")]
        [EmailAddress(ErrorMessage = "{0} must be a valid email address")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(25, ErrorMessage = "{0} must be {2} - {1} characters", MinimumLength = 8)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(25, ErrorMessage = "{0} must be {2} - {1} characters", MinimumLength = 8)]
        [Compare("Password", ErrorMessage = "Password must match")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Mobile")]
        [StringLength(25, ErrorMessage = "{0} may have max. {1} characters")]
        [Required]
        public string Mobile { get; set; }
    }
}

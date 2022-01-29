using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(25, ErrorMessage = "{0} must be {2} - {1} characters", MinimumLength = 8)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(25, ErrorMessage = "{0} must be {2} - {1} characters", MinimumLength = 8)]
        [Compare("Password", ErrorMessage = "Password must match")]
        public string ConfirmPassword { get; set; }
    }
}

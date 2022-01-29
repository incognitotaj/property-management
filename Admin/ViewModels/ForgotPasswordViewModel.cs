using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(350, ErrorMessage = "{0} must not exceeds {1} characters")]
        [EmailAddress]
        public string Email { get; set; }
        
    }
}

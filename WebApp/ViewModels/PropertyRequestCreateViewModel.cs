using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class PropertyRequestCreateViewModel
    {
        [Required]
        public int PropertyId { get; set; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "{0} is required")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Mobile")]
        [Required(ErrorMessage = "{0} is required")]
        public string Mobile { get; set; }

        [Display(Name = "Message")]
        [Required(ErrorMessage = "{0} is required")]
        public string Message { get; set; }
    }
}

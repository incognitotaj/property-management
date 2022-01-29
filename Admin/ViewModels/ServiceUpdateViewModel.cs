using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Admin.ViewModels
{
    public class ServiceUpdateViewModel
    {
        [Required]
        public int ServiceId { get; set; }
        
        [Display(Name = "Name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} must be {2} - {1} characters", MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(2000, ErrorMessage = "{0} must be {2} - {1} characters", MinimumLength = 3)]
        public string Description { get; set; }

        public byte[] CurrentPhoto { get; set; }
        public bool IsActive { get; set; }
    }
}

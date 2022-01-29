using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.ViewModels
{
    public class PropertyCreateViewModel
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(250, ErrorMessage = "{0} must be {2} - {1} characters", MinimumLength = 3)] 
        public string Title { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(2000, ErrorMessage = "{0} must be {2} - {1} characters", MinimumLength = 3)]
        public string Description { get; set; }

        [Display(Name = "Property Type")]
        [Required(ErrorMessage = "{0} is required")]
        public int PropertyTypeId { get; set; }
        public IEnumerable<SelectListItem> PropertyTypes { get; set; }

        [Display(Name = "Purpose")]
        [Required(ErrorMessage = "{0} is required")] 
        public int PurposeId { get; set; }
        public IEnumerable<SelectListItem> Purposes { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "{0} is required")] 
        public int CityId { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "{0} is required")] 
        public string Address { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "{0} is required")] 
        public double Price { get; set; }

        [Display(Name = "Land Area")]
        [Required(ErrorMessage = "{0} is required")] 
        public double LandArea { get; set; }

        [Display(Name = "Area Unit")]
        [Required(ErrorMessage = "{0} is required")] 
        public int AreaUnitId { get; set; }
        public IEnumerable<SelectListItem> AreaUnits { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(250, ErrorMessage = "{0} must be {2} - {1} characters", MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(250, ErrorMessage = "{0} must be max of {1} characters")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Mobile")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(25, ErrorMessage = "{0} must be max of {1} characters")]
        public string Mobile { get; set; }

        public bool IsActive { get; set; }
        public bool IsFeatured { get; set; }

        [Display(Name = "Bed Rooms")]
        [Required(ErrorMessage = "{0} is required")]
        public int BedRooms { get; set; }

        [Display(Name = "Bath Rooms")]
        [Required(ErrorMessage = "{0} is required")]
        public int BathRooms { get; set; }

        [Display(Name = "Floors")]
        [Required(ErrorMessage = "{0} is required")]
        public int Floors { get; set; }

        [Display(Name = "Kitchens")]
        [Required(ErrorMessage = "{0} is required")]
        public int Kitchens { get; set; }

        public bool IsSold { get; set; }

    }
}

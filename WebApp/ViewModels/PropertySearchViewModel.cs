using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class PropertySearchViewModel
    {
        public string Title { get; set; }

        public int AreaFrom { get; set; }
        public int AreaTo { get; set; }

        public int PriceFrom { get; set; }
        public int PriceTo { get; set; }

        public int AreaUnitId { get; set; }
        public IEnumerable<SelectListItem> AreaUnits { get; set; }

        public int CityId { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }

        public int PropertyTypeId { get; set; }
        public IEnumerable<SelectListItem> PropertyTypes { get; set; }

        public int PurposeId { get; set; }
        public IEnumerable<SelectListItem> Purposes { get; set; }

        public int BedId { get; set; }
        public IEnumerable<SelectListItem> Beds { get; set; }

        public int BathId { get; set; }
        public IEnumerable<SelectListItem> Baths { get; set; }
    }
}

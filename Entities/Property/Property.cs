using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Property
    {
        public int PropertyId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int PropertyTypeId { get; set; }
        public virtual PropertyType PropertyType { get; set; }
        
        
        public int PurposeId { get; set; }
        public virtual Purpose Purpose { get; set; }
        
        public int CityId { get; set; }
        public virtual City City { get; set; }

        public virtual ICollection<PropertyPhoto> PropertyPhotos { get; set; }
        public virtual ICollection<PropertyRequest> PropertyRequests { get; set; }

        public string Address { get; set; }
        public double Price { get; set; }
        public double LandArea { get; set; }
        public int AreaUnitId { get; set; }
        public virtual AreaUnit AreaUnit { get; set; }

        public Property()
        {
            PropertyRequests = new HashSet<PropertyRequest>();
            PropertyPhotos = new HashSet<PropertyPhoto>();
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTime Created { get; set; }
        public bool IsActive { get; set; }
        public byte[] Photo { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsSold { get; set; }

        public int BedRooms { get; set; }
        public int BathRooms { get; set; }
        public int Floors { get; set; }
        public int Kitchens{ get; set; }

        // Contact details
        //public string ContactEmail { get; set; }
        //public string ContactMobile { get; set; }
    }
}

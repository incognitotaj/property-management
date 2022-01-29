using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class City
    {
        public int CityId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Property> Properties { get; set; }

        public City()
        {
            Properties = new HashSet<Property>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class PropertyType
    {
        public int PropertyTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Property> Properties { get; set; }

        public PropertyType()
        {
            Properties = new HashSet<Property>();
        }
    }
}

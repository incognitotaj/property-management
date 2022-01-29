using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class AreaUnit
    {
        public int AreaUnitId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Property> Properties { get; set; }

        public AreaUnit()
        {
            Properties = new HashSet<Property>();
        }
    }
}

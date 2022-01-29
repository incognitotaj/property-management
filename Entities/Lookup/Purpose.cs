using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Purpose
    {
        public int PurposeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Property> Properties { get; set; }

        public Purpose()
        {
            Properties = new HashSet<Property>();
        }
    }
}

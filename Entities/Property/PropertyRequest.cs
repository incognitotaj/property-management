using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class PropertyRequest
    {
        public int PropertyRequestId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Message { get; set; }

        public int PropertyId { get; set; }
        public virtual Property Property { get; set; }
        public bool IsRead { get; set; }
        public DateTime Created { get; set; }
    }
}

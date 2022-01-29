using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class ServiceRequest
    {
        public int ServiceRequestId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Message { get; set; }

        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }
        public bool IsRead { get; set; }
        public DateTime Created { get; set; }
    }
}

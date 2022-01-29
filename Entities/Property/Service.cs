using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Photo { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }

        public ICollection<ServiceRequest> ServiceRequests { get; set; }

        public Service()
        {
            ServiceRequests = new HashSet<ServiceRequest>();
        }
    }
}

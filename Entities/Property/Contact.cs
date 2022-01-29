using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Message { get; set; }
        public DateTime Created { get; set; }
        public bool IsActive { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public byte[] Photo { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public Blog()
        {
            Comments = new HashSet<Comment>();
        }
    }
}

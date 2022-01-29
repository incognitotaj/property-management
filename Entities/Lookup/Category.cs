using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }
        public Category()
        {
            Blogs = new HashSet<Blog>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }

        public DateTime Created { get; set; }
        public bool IsActive { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class PropertyPhoto
    {
        public int PropertyPhotoId { get; set; }
        public int PropertyId { get; set; }
        public virtual Property Property { get; set; }
        public byte[] Photo { get; set; }
        public DateTime Created { get; set; }
        public bool IsActive { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace PhotoViewingBot
{
    public partial class Tag
    {
        public Tag()
        {
            Photos = new HashSet<Photo>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
    }
}

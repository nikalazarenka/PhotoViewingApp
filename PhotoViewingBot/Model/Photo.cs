using System;
using System.Collections.Generic;

#nullable disable

namespace PhotoViewingBot
{
    public partial class Photo
    {
        public int Id { get; set; }
        public string PhotoData { get; set; }
        public int TagId { get; set; }

        public virtual Tag Tag { get; set; }
    }
}

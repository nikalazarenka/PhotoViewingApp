using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoViewing.Data.Models
{
    public class Tag
    {
        public Tag()
        {
            Photos = new HashSet<Photo>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
    }
}

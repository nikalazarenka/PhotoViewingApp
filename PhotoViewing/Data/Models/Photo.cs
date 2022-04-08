using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoViewing.Data.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        public string PhotoData { get; set; }
        public int TagId { get; set; }

        [ForeignKey("TagId")]
        public virtual Tag Tag { get; set; }
    }
}

using PhotoViewing.Data.Models;
using System.Collections.Generic;

namespace PhotoViewing.ViewModels
{
    public class PhotosViewModel
    {
        public IEnumerable<Photo> Photos { get; set; }
        public PhotoViewModel PhotoViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
    }
}

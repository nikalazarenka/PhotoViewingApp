using System.Collections.Generic;
using PhotoViewing.Data.Models;

namespace PhotoViewing.Data.Interfaces
{
    public interface IPhotos
    {
        IEnumerable<Photo> Photos { get; }

        Photo getPhotoById(int? photoId);

        public void Delete(int? id);
    }
}

using PhotoViewing.Data.Interfaces;
using PhotoViewing.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace PhotoViewing.Data.Repository
{
    public class PhotoRepository : IPhotos
    {
        private readonly AppDbContext appDbContext;
        public PhotoRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public IEnumerable<Photo> Photos => appDbContext.Photos;

        public Photo getPhotoById(int? photoId) => appDbContext.Photos.FirstOrDefault(t => t.Id == photoId);

        public void Delete(int? id)
        {
            Photo photo = appDbContext.Photos.FirstOrDefault(p => p.Id == id);
            if (photo != null)
            {
                appDbContext.Photos.Remove(photo);
                appDbContext.SaveChanges();
            }
        }

    }
}

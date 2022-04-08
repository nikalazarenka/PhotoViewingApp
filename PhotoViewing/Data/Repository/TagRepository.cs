using PhotoViewing.Data.Interfaces;
using PhotoViewing.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace PhotoViewing.Data.Repository
{
    public class TagRepository : ITags
    {
        private readonly AppDbContext appDbContext;
        public TagRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public IEnumerable<Tag> Tags => appDbContext.Tags;

        public Tag getTagById(int? tagId) => appDbContext.Tags.FirstOrDefault(t => t.Id == tagId);
    }
}

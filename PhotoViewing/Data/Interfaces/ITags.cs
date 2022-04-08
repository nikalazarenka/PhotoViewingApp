using PhotoViewing.Data.Models;
using System.Collections.Generic;

namespace PhotoViewing.Data.Interfaces
{
    public interface ITags
    {
        IEnumerable<Tag> Tags { get; }
        Tag getTagById(int? tagId);
    }
}

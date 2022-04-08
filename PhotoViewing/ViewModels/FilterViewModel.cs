using Microsoft.AspNetCore.Mvc.Rendering;
using PhotoViewing.Data.Models;
using System.Collections.Generic;


namespace PhotoViewing.ViewModels
{
    public class FilterViewModel
    {
        public SelectList Tags { get; set; }
        public int? SelectedTag { get; set; }

        public FilterViewModel(List<Tag> tags, int? tag)
        {
            tags.Insert(0, new Tag { Name = "All", Id = 0 });

            Tags = new SelectList(tags, "Id", "Name", tag);

            SelectedTag = tag;
        }
    }
}

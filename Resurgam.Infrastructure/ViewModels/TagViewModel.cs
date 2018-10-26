using Resurgam.AppCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Infrastructure.ViewModels
{
    public class TagViewModel
    {
        public TagViewModel() { }
        public TagViewModel(Tag tag)
        {
            ProjectId = tag.ProjectId;
            TagId = tag.TagId;
            TagName = tag.Name;
        }
        public Guid ProjectId { get; set; }
        public Guid TagId { get; set; }
        public string TagName { get; set; }
    }
}

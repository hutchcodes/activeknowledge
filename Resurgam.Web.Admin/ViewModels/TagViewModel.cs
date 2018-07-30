using Resurgam.AppCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Web.Admin.ViewModels
{
    public class TagViewModel
    {
        public TagViewModel(Tag tag)
        {
            ProjectId = tag.ProjectId;
            TagId = tag.Id;
            TagName = tag.Name;
        }
        public int ProjectId { get; set; }
        public int TagId { get; set; }
        public string TagName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Resurgam.AppCore.Entities.Interfaces;

namespace Resurgam.AppCore.Entities
{
    public class CategoryTopic 
    {
        public Guid ProjectId { get; set; }
        public Guid ParentCategoryId { get; set; }
        public int Order { get; set; }
        public Guid TopicId { get; set; }
        public Topic Topic { get; set; }
    }
}

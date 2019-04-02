using System;
using System.Collections.Generic;
using System.Text;
using AKS.AppCore.Entities.Interfaces;

namespace AKS.AppCore.Entities
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

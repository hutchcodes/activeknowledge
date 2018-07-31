using System;
using System.Collections.Generic;
using System.Text;
using Resurgam.AppCore.Entities.Interfaces;

namespace Resurgam.AppCore.Entities
{
    public class CategoryTopic 
    {
        public int ProjectId { get; set; }
        public int ParentCategoryId { get; set; }
        public int Order { get; set; }
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
    }
}

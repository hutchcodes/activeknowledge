using System;
using System.Collections.Generic;
using System.Text;

namespace Resurgam.AppCore.Entities
{
    public class CategoryTopic : BaseEntity
    {
        public int TopicId { get; set; }
        public int Order { get; set; }
    }
}

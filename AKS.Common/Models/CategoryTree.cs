using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Common.Models
{
    public class CategoryTree
    {
        public Guid ProjectId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public int Order { get; set; }
        public string? Name { get; set; }
        public List<CategoryTree> Categories { get; set; } = new List<CategoryTree>();
        public List<TopicLink> Topics { get; set; } = new List<TopicLink>();
    }
}

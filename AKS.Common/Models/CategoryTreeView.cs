using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Common.Models
{
    public class CategoryTreeView
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<CategoryTreeView> Categories { get;} = new List<CategoryTreeView>();
        public List<TopicLink> Topics { get; } = new List<TopicLink>();
    }
}

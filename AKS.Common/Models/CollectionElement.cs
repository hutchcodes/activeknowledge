using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Common.Models
{
    public class CollectionElement
    {
        public Guid ProjectId { get; set; }
        public Guid CollectionElementId { get; set; }
        public string Name { get; set; }
        public List<TopicView> ElementTopics { get; set; } = new List<TopicView>();
    }
}

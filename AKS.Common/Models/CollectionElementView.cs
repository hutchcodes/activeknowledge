using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Common.Models
{
    public class CollectionElementView
    {
        public Guid ProjectId { get; set; }
        public Guid TopicId { get; set; }
        public Guid CollectionElementId { get; set; }
        public string? Name { get; set; }
        public List<CollectionElementTopicView> ElementTopics { get; set; } = new List<CollectionElementTopicView>();
    }
}

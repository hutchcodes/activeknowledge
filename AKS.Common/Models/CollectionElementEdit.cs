using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Common.Models
{
    public class CollectionElementEdit
    {
        public Guid ProjectId { get; set; }
        public Guid TopicId { get; set; }
        public Guid CollectionElementId { get; set; }
        public string? Name { get; set; }
        public List<CollectionElementTopicList> ElementTopics { get; set; } = new List<CollectionElementTopicList>();
    }
}

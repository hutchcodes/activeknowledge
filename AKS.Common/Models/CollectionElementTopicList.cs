using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Common.Models
{
    public class CollectionElementTopicList
    {
        public Guid CollectionElementId { get; set; }
        public Guid ProjectId { get; set; }
        public Guid TopicId { get; set; }
        public TopicList? Topic { get; set; }
        public int Order { get; set; }
    }
}

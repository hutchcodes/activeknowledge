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
        public string CollectionElementName { get; set; }
        public List<TopicView> Topics { get; } = new List<TopicView>();
    }
}

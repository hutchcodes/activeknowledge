using AKS.AppCore.Specifications;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace AKS.AppCore.Entities
{
    public class CollectionElementTopic
    {
        //public int CollectionElementTopicId {get;set;}
        public Guid CollectionElementId { get; set; }
        public CollectionElement CollectionElement { get; set; }
        public Guid ProjectId { get; set; }
        public Guid? TopicId { get; set; }
        public Topic Topic { get; set; }
        public int Order { get; set; }        
    }
}

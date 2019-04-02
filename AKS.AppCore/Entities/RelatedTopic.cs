using AKS.AppCore.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AKS.AppCore.Entities
{
    public class RelatedTopic : IReferencedTopic
    {
        public Guid ProjectId { get; set; }
        public Guid ParentTopicId { get; set; }
        public Topic ParentTopic { get; set; }
        public Guid ChildTopicId { get; set; }
        public Topic ChildTopic { get; set; }
    }
}

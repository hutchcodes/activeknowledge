using Resurgam.AppCore.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resurgam.AppCore.Entities
{
    public class RelatedTopic : BaseEntity, IReferencedTopic
    {
        public int ProjectId { get; set; }
        public int ParentTopicId { get; set; }
        public Topic ParentTopic { get; set; }
        public int ChildTopicId { get; set; }
        public Topic ChildTopic { get; set; }
    }
}

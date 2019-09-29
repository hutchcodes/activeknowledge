using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AKS.AppCore.Entities
{
    [Table("CollectionElementTopic")]
    public partial class CollectionElementTopic : BaseEntity
    {
        public Guid CollectionElementId { get; set; }
        public Guid ProjectId { get; set; }
        public Guid TopicId { get; set; }
        public int Order { get; set; }

        public virtual CollectionElement? CollectionElement { get; set; }
        public virtual Topic? Topic { get; set; }
    }
}

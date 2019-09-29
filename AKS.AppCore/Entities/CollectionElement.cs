using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AKS.AppCore.Entities
{
    [Table("CollectionElement")]
    public partial class CollectionElement : BaseEntity
    {
        public CollectionElement()
        {
            CollectionElementTopics = new HashSet<CollectionElementTopic>();
        }

        public Guid CollectionElementId { get; set; }
        public Guid ProjectId { get; set; }
        public Guid TopicId { get; set; }
        public string? Name { get; set; }

        public virtual Topic? Topic { get; set; }
        public virtual ICollection<CollectionElementTopic> CollectionElementTopics { get; set; }
    }
}

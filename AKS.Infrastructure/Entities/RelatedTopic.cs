using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AKS.Infrastructure.Entities
{
    [Table("RelatedTopic")]
    public partial class RelatedTopic : BaseEntity
    {
        public Guid ProjectId { get; set; }
        public Guid ParentTopicId { get; set; }
        public Guid ChildTopicId { get; set; }

        public virtual Topic? ParentTopic { get; set; }
        public virtual Topic? ChildTopic { get; set; }
    }
}

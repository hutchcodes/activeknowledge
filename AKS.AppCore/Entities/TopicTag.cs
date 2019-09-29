using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AKS.AppCore.Entities
{
    [Table("TopicTag")]
    public partial class TopicTag : BaseEntity
    {
        public Guid ProjectId { get; set; }
        public Guid TopicId { get; set; }
        public Guid TagId { get; set; }

        public virtual Tag? Tag { get; set; }
        public virtual Topic? Topic { get; set; }
    }
}

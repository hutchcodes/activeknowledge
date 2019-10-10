using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AKS.Infrastructure.Entities
{
    [Table("Tag")]
    public partial class Tag : BaseEntity
    {
        public Tag()
        {
            TopicTags = new HashSet<TopicTag>();
        }

        public Guid TagId { get; set; }
        public Guid ProjectId { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<TopicTag> TopicTags { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AKS.Infrastructure.Entities
{
    [Table("CategoryTopic")]
    public partial class CategoryTopic: BaseEntity
    {
        public Guid ProjectId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid TopicId { get; set; }
        public int Order { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Topic? Topic { get; set; }
    }
}

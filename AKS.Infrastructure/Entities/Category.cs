using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AKS.Infrastructure.Entities
{
    [Table("Category")]
    public partial class Category : BaseEntity
    {
        public Guid CategoryId { get; set; }
        public Guid ProjectId { get; set; }
        public string? Name { get; set; }
        public int Order { get; set; }
        public Guid? ParentCategoryId { get; set; }

        public virtual Category? ParentCategory { get; set; }
        public virtual ICollection<CategoryTopic> CategoryTopics { get; set; } = new HashSet<CategoryTopic>();
        public virtual ICollection<Category> Categories { get; set; } = new HashSet<Category>();
    }
}

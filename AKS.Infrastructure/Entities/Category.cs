using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AKS.Infrastructure.Entities
{
    [Table("Category")]
    public partial class Category : BaseEntity
    {
        public Category()
        {
            CategoryTopics = new HashSet<CategoryTopic>();
            Categories = new HashSet<Category>();
        }

        public Guid CategoryId { get; set; }
        public Guid ProjectId { get; set; }
        public string? Name { get; set; }
        public int Order { get; set; }
        public Guid? ParentCategoryId { get; set; }

        public Category? ParentCategory { get; set; }
        public ICollection<CategoryTopic> CategoryTopics { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}

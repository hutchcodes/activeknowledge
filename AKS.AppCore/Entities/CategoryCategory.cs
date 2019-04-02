using System;
using System.Collections.Generic;
using System.Text;
using AKS.AppCore.Entities.Interfaces;

namespace AKS.AppCore.Entities
{
    public class CategoryCategory : BaseEntity
    {
        public Guid ParentCategoryId { get; set; }
        public int Order { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

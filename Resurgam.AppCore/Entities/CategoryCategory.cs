using System;
using System.Collections.Generic;
using System.Text;
using Resurgam.AppCore.Entities.Interfaces;

namespace Resurgam.AppCore.Entities
{
    public class CategoryCategory : BaseEntity
    {
        public Guid ParentCategoryId { get; set; }
        public int Order { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

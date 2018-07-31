using System;
using System.Collections.Generic;
using System.Text;
using Resurgam.AppCore.Entities.Interfaces;

namespace Resurgam.AppCore.Entities
{
    public class CategoryCategory : BaseEntity
    {
        public int ParentCategoryId { get; set; }
        public int Order { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

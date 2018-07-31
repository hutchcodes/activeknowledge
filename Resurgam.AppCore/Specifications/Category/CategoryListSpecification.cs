using System;
using System.Collections.Generic;
using System.Text;
using Resurgam.AppCore.Entities;

namespace Resurgam.AppCore.Specifications
{
    public class CategoryListSpecification : BaseSpecification<Category>
    {
        public CategoryListSpecification(int projectId) : base(x => x.ProjectId == projectId && x.ParentCategoryId == null)
        {
            AddInclude($"{nameof(Category.Topics)}.{nameof(CategoryTopic.Topic)}");
            AddInclude($"{nameof(Category.Categories)}.{nameof(Category.Topics)}.{nameof(CategoryTopic.Topic)}");
            AddInclude($"{nameof(Category.Categories)}.{nameof(Category.Categories)}.{nameof(Category.Topics)}.{nameof(CategoryTopic.Topic)}");
            AddInclude($"{nameof(Category.Categories)}.{nameof(Category.Categories)}.{nameof(Category.Categories)}.{nameof(Category.Topics)}.{nameof(CategoryTopic.Topic)}");
        }
    }
}

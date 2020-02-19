using System;
using System.Collections.Generic;
using System.Text;
using AKS.Infrastructure.Entities;

namespace AKS.Infrastructure.Specifications
{
    public class CategoryListSpecification : BaseSpecification<Category>
    {
        public CategoryListSpecification(Guid projectId) : base(x => x.ProjectId == projectId)
        {
            AddInclude($"{nameof(Category.CategoryTopics)}.{nameof(CategoryTopic.Topic)}");
        }

        public CategoryListSpecification(Guid projectId, Guid parentCategoryId) : base(x => x.ProjectId == projectId && x.ParentCategoryId == parentCategoryId)
        {
            AddInclude($"{nameof(Category.CategoryTopics)}.{nameof(CategoryTopic.Topic)}");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using AKS.Infrastructure.Entities;

namespace AKS.Infrastructure.Specifications
{
    public class CategoryListSpecification : BaseSpecification<Category>
    {
        public CategoryListSpecification(Guid projectId) : base(x => x.ProjectId == projectId && x.ParentCategoryId == null)
        {
            AddInclude($"{nameof(Category.CategoryTopics)}.{nameof(CategoryTopic.Topic)}");
            AddInclude($"{nameof(Category.Categories)}.{nameof(Category.CategoryTopics)}.{nameof(CategoryTopic.Topic)}");
            AddInclude($"{nameof(Category.Categories)}.{nameof(Category.Categories)}.{nameof(Category.CategoryTopics)}.{nameof(CategoryTopic.Topic)}");
            AddInclude($"{nameof(Category.Categories)}.{nameof(Category.Categories)}.{nameof(Category.Categories)}.{nameof(Category.CategoryTopics)}.{nameof(CategoryTopic.Topic)}");
        }
    }
}

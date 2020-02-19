using System;
using System.Collections.Generic;
using System.Text;
using AKS.Infrastructure.Entities;

namespace AKS.Infrastructure.Specifications
{
    public class CategorySpecification : BaseSpecification<Category>
    {
        public CategorySpecification(Guid projectId, Guid categoryId) : base(x => x.ProjectId == projectId && x.CategoryId == categoryId)
        {
            AddInclude($"{nameof(Category.CategoryTopics)}.{nameof(CategoryTopic.Topic)}");
        }
    }
}

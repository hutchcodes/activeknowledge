using System;
using System.Collections.Generic;
using System.Text;
using AKS.Infrastructure.Entities;

namespace AKS.Infrastructure.Specifications
{
    public class CategoryTopicSpecification : BaseSpecification<CategoryTopic>
    {
        public CategoryTopicSpecification(Guid projectId, Guid categoryId, Guid topicId) : base(x => x.ProjectId == projectId && x.CategoryId == categoryId && x.TopicId == topicId)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AKS.Infrastructure.Entities;

namespace AKS.Infrastructure.Specifications
{
    public class TopicSearchSpecification : BaseSpecification<Topic>
    {
        public TopicSearchSpecification(Guid projectId, Guid? categoryId, string searchString) : 
            base(x => 
                x.ProjectId == projectId
                && (string.IsNullOrWhiteSpace(searchString) || x.Title!.Contains(searchString) || x.Description!.Contains(searchString) || x.Content!.Contains(searchString))
                && (!categoryId.HasValue || x.CategoryTopics.Any(x=> x.CategoryId == categoryId.Value))
            )
        {
        }
    }
}

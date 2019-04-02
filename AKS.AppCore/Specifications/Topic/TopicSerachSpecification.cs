using System;
using System.Collections.Generic;
using System.Text;
using AKS.AppCore.Entities;

namespace AKS.AppCore.Specifications
{
    public class TopicSearchSpecification : BaseSpecification<Topic>
    {
        public TopicSearchSpecification(Guid projectId, Guid? categoryId, string searchString) : 
            base(x => 
                x.ProjectId == projectId
                && (string.IsNullOrWhiteSpace(searchString) || x.Name.Contains(searchString) || x.Description.Contains(searchString) || x.TopicContent.Contains(searchString))
            )
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Resurgam.AppCore.Entities;

namespace Resurgam.AppCore.Specifications
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

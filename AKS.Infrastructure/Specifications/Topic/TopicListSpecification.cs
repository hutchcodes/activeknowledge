using System;
using System.Collections.Generic;
using System.Text;
using AKS.Infrastructure.Entities;

namespace AKS.Infrastructure.Specifications
{
    public class TopicListSpecification : BaseSpecification<Topic>
    {
        public TopicListSpecification(Guid projectId) : base(x => x.ProjectId == projectId)
        {
        }
    }
}

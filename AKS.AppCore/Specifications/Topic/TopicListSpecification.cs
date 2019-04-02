using System;
using System.Collections.Generic;
using System.Text;
using AKS.AppCore.Entities;

namespace AKS.AppCore.Specifications
{
    public class TopicListSpecification : BaseSpecification<Topic>
    {
        public TopicListSpecification(Guid projectId) : base(x => x.ProjectId == projectId)
        {
        }
    }
}

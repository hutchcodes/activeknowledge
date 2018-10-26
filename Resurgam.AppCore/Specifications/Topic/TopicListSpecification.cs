using System;
using System.Collections.Generic;
using System.Text;
using Resurgam.AppCore.Entities;

namespace Resurgam.AppCore.Specifications
{
    public class TopicListSpecification : BaseSpecification<Topic>
    {
        public TopicListSpecification(Guid projectId) : base(x => x.ProjectId == projectId)
        {
        }
    }
}

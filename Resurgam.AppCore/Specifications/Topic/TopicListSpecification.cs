using System;
using System.Collections.Generic;
using System.Text;
using Resurgam.AppCore.Entities;

namespace Resurgam.AppCore.Specifications
{
    public class TopicListSpecification : BaseSpecification<Topic>
    {
        public TopicListSpecification(int projectId) : base(x => x.ProjectId == projectId)
        {
        }
    }
}

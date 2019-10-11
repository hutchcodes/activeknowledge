using System;
using System.Collections.Generic;
using System.Text;
using AKS.Infrastructure.Entities;

namespace AKS.Infrastructure.Specifications
{
    public class ProjectSpecification : BaseSpecification<Project>
    {
        public ProjectSpecification(Guid projectId) : base(x => x.ProjectId == projectId)
        {

        }
    }
}

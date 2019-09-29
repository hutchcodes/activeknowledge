using System;
using System.Collections.Generic;
using System.Text;
using AKS.AppCore.Entities;

namespace AKS.AppCore.Specifications
{
    public class ProjectHeaderSpecification : BaseSpecification<Project>
    {
        public ProjectHeaderSpecification(Guid projectId) : base(x => x.ProjectId == projectId)
        {
            this.Includes.Add(x => x.Customer!);
        }
    }
}

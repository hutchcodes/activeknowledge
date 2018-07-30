using System;
using System.Collections.Generic;
using System.Text;
using Resurgam.AppCore.Entities;

namespace Resurgam.AppCore.Specifications
{
    public class ProjectHeaderSpecification : BaseSpecification<Project>
    {
        public ProjectHeaderSpecification(int projectId) : base(x => x.Id == projectId)
        {
            this.Includes.Add(x => x.Customer);
        }
    }
}

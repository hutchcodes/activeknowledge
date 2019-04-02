using System;
using System.Collections.Generic;
using System.Text;
using AKS.AppCore.Entities;

namespace AKS.AppCore.Specifications
{
    public class ProjectListSpecification : BaseSpecification<Project>
    {
        public ProjectListSpecification(Guid customerId) : base(x => x.CustomerId == customerId)
        {
        }
    }
}

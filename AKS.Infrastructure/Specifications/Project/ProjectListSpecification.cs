using System;
using System.Collections.Generic;
using System.Text;
using AKS.Infrastructure.Entities;

namespace AKS.Infrastructure.Specifications
{
    public class ProjectListSpecification : BaseSpecification<Project>
    {
        public ProjectListSpecification(Guid customerId) : base(x => x.CustomerId == customerId)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Resurgam.AppCore.Entities;

namespace Resurgam.AppCore.Specifications
{
    public class ProjectListSpecification : BaseSpecification<Project>
    {
        public ProjectListSpecification(int customerId) : base(x => x.CustomerId == customerId)
        {
        }
    }
}

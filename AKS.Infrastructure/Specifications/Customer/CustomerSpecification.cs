using System;
using System.Collections.Generic;
using System.Text;
using AKS.Infrastructure.Entities;

namespace AKS.Infrastructure.Specifications
{
    public class CustomerSpecification : BaseSpecification<Customer>
    {
        public CustomerSpecification(Guid customerId) : base(x => x.CustomerId == customerId)
        {
        }
    }
}

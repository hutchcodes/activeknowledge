using System;
using System.Collections.Generic;
using System.Text;
using AKS.Infrastructure.Entities;

namespace AKS.Infrastructure.Specifications
{
    public class CustomerHeaderSpecification : BaseSpecification<Customer>
    {
        public CustomerHeaderSpecification(Guid customerId) : base(x => x.CustomerId == customerId)
        {
        }
    }
}

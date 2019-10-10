using System;
using System.Collections.Generic;
using System.Text;
using AKS.AppCore.Entities;

namespace AKS.AppCore.Specifications
{
    public class CustomerSpecification : BaseSpecification<Customer>
    {
        public CustomerSpecification(Guid customerId) : base(x => x.CustomerId == customerId)
        {
        }
    }
}

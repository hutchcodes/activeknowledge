using System;
using System.Collections.Generic;
using System.Text;
using AKS.AppCore.Entities;

namespace AKS.AppCore.Specifications
{
    public class CustomerHeaderSpecification : BaseSpecification<Customer>
    {
        public CustomerHeaderSpecification(Guid customerId) : base(x => x.CustomerId == customerId)
        {
        }
    }
}

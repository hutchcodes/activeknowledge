using System;
using System.Collections.Generic;
using System.Text;
using Resurgam.AppCore.Entities;

namespace Resurgam.AppCore.Specifications
{
    public class CustomerHeaderSpecification : BaseSpecification<Customer>
    {
        public CustomerHeaderSpecification(int customerId) : base(x => x.Id == customerId)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AKS.Common.Models;

namespace AKS.Infrastructure.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerEdit> GetCustomerForEdit(Guid customerId);
        Task<CustomerEdit> UpdateCustomer(CustomerEdit customerEdit);
    }
}

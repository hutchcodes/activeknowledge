using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AKS.Infrastructure.Interfaces
{
    public interface IGroupService
    {
        Task AddCustomerAdminWithUser(Guid customerId, Guid userId);
    }
}

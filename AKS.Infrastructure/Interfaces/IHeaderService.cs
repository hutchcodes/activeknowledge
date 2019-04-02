using System;
using System.Threading.Tasks;
using AKS.Common.Models;

namespace AKS.Infrastructure.Interfaces
{
    public interface IHeaderService
    {
        Task<HeaderNavView> GetHeaderForProjectAsync(Guid projectId);
        Task<HeaderNavView> GetHeaderForCustomerAsync(Guid customerId);
    }
}

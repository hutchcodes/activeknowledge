using AKS.Common.Models;
using AKS.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Infrastructure.Interfaces
{
    public interface IHeaderService
    {
        Task<HeaderNavView> GetHeaderForProjectAsync(Guid projectId);
        Task<HeaderNavView> GetHeaderForCustomerAsync(Guid customerId);
    }
}

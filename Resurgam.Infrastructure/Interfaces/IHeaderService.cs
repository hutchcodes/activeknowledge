using Resurgam.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Infrastructure.Interfaces
{
    public interface IHeaderService
    {
        Task<HeaderNavViewModel> GetHeaderForProjectAsync(Guid projectId);
        Task<HeaderNavViewModel> GetHeaderForCustomerAsync(Guid customerId);
    }
}

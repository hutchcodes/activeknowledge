using Resurgam.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Web.Admin.Interfaces
{
    public interface IHeaderService
    {
        Task<HeaderNavViewModel> GetHeaderForProjectAsync(int projectId);
        Task<HeaderNavViewModel> GetHeaderForCustomerAsync(int customerId);
    }
}

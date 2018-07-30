using Resurgam.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Web.Admin.Interfaces
{
    public interface IProjectService
    {
        Task<List<ProjectListViewModel>> GetProjetListForDisplayAsync(int customerId);
    }
}

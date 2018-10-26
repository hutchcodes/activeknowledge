using Resurgam.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Infrastructure.Interfaces
{
    public interface IProjectService
    {
        Task<List<ProjectListViewModel>> GetProjetListForDisplayAsync(Guid customerId);
    }
}

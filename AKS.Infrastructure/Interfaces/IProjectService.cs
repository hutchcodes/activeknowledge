using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AKS.Common.Models;

namespace AKS.Infrastructure.Interfaces
{
    public interface IProjectService
    {
        Task<List<ProjectList>> GetProjetListForDisplayAsync(Guid customerId);
    }
}

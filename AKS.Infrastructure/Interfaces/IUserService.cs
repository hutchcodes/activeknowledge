using AKS.Common.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AKS.Infrastructure.Interfaces
{
    public interface IUserService
    {
        Task<AKSUser> GetAuthenticatedUser(Guid UserId);
        Task CreateUpdateUser(ClaimsPrincipal principal);
    }
}

using Resurgam.AppCore.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Resurgam.Infrastructure.Data.Security
{
    public interface ISecurityRepository
    {
        Task SaveUserInfo(User user);

        Task<User> GetUser(Guid userId);
    }
}

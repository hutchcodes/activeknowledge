using AKS.Common.Enums;
using AKS.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Api.Build.Cache
{
    public static class UserPermisionsCache
    {
        public static Dictionary<Guid, EntityPermission> CustomerPermissions { get; } = new Dictionary<Guid, EntityPermission>();

        public static void AddUserCustomerPermissions(Guid userId, Guid customerId, bool canRead, bool canUpdate, bool canDelete)
        {

        }
    }
}

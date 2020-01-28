using AKS.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AKS.Common.Models
{
    public class EntityPermission
    {
        public EntityPermission(Guid entityId, PermissionFlag permissions)
        {
            EntityId = entityId;
            Permissions = permissions;
        }
        public Guid EntityId { get; }
        public PermissionFlag Permissions { get; }
        public bool CanRead { get { return Permissions.HasFlag(PermissionFlag.Read); } }
        public bool CanUpdate { get { return Permissions.HasFlag(PermissionFlag.Update); } }
        public bool CanDelete { get { return Permissions.HasFlag(PermissionFlag.Delete); } }
    }
}

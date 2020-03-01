using System;
using System.Collections.Generic;
using System.Text;

namespace AKS.Infrastructure.Entities
{
    public class Group : BaseEntity
    {
        public Group()
        {
            UserGroups = new HashSet<UserGroup>();
        }

        public Guid GroupId { get; set; }
        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public string GroupName { get; set; } = "";
        public bool IsActive { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
        public virtual ICollection<GroupProjectPermission> GroupProjectPermissions { get; set; }

        public bool CanEditCustomer { get; set; }
        public bool CanManageAccess { get; set; }
        public bool CanCreateProject { get; set; }
    }
}

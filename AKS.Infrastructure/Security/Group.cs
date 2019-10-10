using System;
using System.Collections.Generic;
using System.Text;

namespace AKS.Infrastructure.Security
{
    public class Group
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public List<User> Users { get; set; } = new List<User>();

        public List<ProjectGroupPermissions> ProjectPermissions { get; set; } = new List<ProjectGroupPermissions>();
    }
}

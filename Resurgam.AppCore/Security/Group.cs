using System;
using System.Collections.Generic;
using System.Text;

namespace Resurgam.AppCore.Security
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<User> Users { get; set; } = new List<User>();

        public List<ProjectGroupPermissions> ProjectPermissions { get; set; } = new List<ProjectGroupPermissions>();
    }
}

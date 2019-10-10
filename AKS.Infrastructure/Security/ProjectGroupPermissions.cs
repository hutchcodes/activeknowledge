using System;
using System.Collections.Generic;
using System.Text;

namespace AKS.Infrastructure.Security
{
    public class ProjectGroupPermissions
    {
        public Guid ProjectId { get; set; }
        public Guid GroupId { get; set; }
        public bool CanView { get; set; }
        public bool CanSearch { get; set; }
        public bool CanEditTopics { get; set; }
        public bool CanManageProject { get; set; }
    }
}

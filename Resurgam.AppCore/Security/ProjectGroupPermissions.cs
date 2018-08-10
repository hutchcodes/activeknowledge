using System;
using System.Collections.Generic;
using System.Text;

namespace Resurgam.AppCore.Security
{
    public class ProjectGroupPermissions
    {
        public int ProjectId { get; set; }
        public Guid GroupId { get; set; }
        public bool CanView { get; set; }
        public bool CanSearch { get; set; }
        public bool CanEditTopics { get; set; }
        public bool CanManageProject { get; set; }
    }
}

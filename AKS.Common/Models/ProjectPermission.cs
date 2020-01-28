using AKS.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AKS.Common.Models
{
    public class ProjectPermission
    {
        public Guid ProjectId { get; set; }
        public PermissionFlag Permissions { get; set; }

        public bool CanRead { get; set; }
        public bool CanEdit { get; set; }
    }
}

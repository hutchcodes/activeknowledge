using System;
using System.Collections.Generic;
using System.Text;

namespace AKS.Infrastructure.Entities
{
    public class GroupProjectPermission
    {
        public Guid GroupId { get; set; }
        public virtual Group? Group { get; set; }
        public Guid ProjectId { get; set; }
        
        public bool CanRead { get; set; }
        public bool CanEdit { get; set; }
    }
}

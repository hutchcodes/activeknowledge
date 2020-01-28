using System;
using System.Collections.Generic;
using System.Text;

namespace AKS.Infrastructure.Entities
{
    public class UserGroup
    {
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }

        public virtual User? User { get; set; }
        public virtual Group? Group { get; set; }
    }
}

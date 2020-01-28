using System;
using System.Collections.Generic;
using System.Text;

namespace AKS.Infrastructure.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            UserGroups = new HashSet<UserGroup>();
        }
        public Guid UserId { get; set; }
        public Guid CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public string DisplayName { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";

        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}

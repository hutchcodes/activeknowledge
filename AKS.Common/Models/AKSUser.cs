using System;
using System.Collections.Generic;
using System.Text;

namespace AKS.Common.Models
{
    public class AKSUser
    {
        public Guid UserId { get; set; }
        public Guid CustomerId { get; set; }
        public string DisplayName { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";

        public List<CustomerPermission> CustomerPermissions { get; set; } = new List<CustomerPermission>();
        public List<ProjectPermission> ProjectPermissions { get; set; } = new List<ProjectPermission>();
    }
}

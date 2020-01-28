using AKS.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AKS.Common.Models
{
    public class CustomerPermission
    {
        public Guid CustomerId { get; set; }
        public bool CanManageAccess { get; set; }
        public bool CanEditCustomer { get; set; }
        public bool CanCreateProject { get; set; }
    }
}

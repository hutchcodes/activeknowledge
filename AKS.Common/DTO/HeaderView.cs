using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Common.DTO
{
    public class HeaderNavView
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLogoURL { get; set; }
        public Guid? ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectLogoURL { get; set; }        
    }
}

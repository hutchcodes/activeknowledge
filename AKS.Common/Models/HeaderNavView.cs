using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Common.Models
{
    public class HeaderNavView
    {
        public Guid CustomerId { get; set; }
        public string? CustomerName { get;set; }
        public string? CustomerLogo { get; set; }
        public Guid? ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectLogo { get; set; }        
    }
}

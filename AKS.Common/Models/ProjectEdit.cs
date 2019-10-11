using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Common.Models
{
    
    public class ProjectEdit
    {
        public Guid ProjectId { get; set; }
        public Guid CustomerId { get; set; }
        public string Name { get; set; } = "";
        public string? LogoFileName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Common.Models
{
    
    public class ProjectEdit
    {
        public Guid ProjectId { get; set; }
        public Guid CustomerId { get; set; }
        
        [Required]
        [StringLength(50)] public string Name { get; set; } = "";
        public string? LogoFileName { get; set; }
    }
}

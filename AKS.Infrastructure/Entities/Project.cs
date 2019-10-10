using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AKS.Infrastructure.Entities
{
    [Table("Project")]
    public partial class Project : BaseEntity
    {
        public Guid ProjectId { get; set; }
        public Guid CustomerId { get; set; }
        public string? Name { get; set; }
        public string? LogoFileName { get; set; }

        public virtual Customer? Customer { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Resurgam.AppCore.Entities
{
    public class Project : BaseEntity
    {
        public Guid ProjectId { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string Name { get; set; }
        public string LogoFileName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace AKS.AppCore.Entities
{
    public class Tag 
    {
        public Guid TagId { get; set; }
        public Guid ProjectId { get; set; }
        public string Name { get; set; }
    }
}

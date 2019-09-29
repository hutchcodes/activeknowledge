using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Common.Models
{
    public class Tag
    {
        public Guid TopicId { get; set; }
        public Guid ProjectId { get; set; }
        public Guid TagId { get; set; }
        public string Name { get; set; } = "";
    }
}

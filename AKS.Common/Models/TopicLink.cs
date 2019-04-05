using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Common.Models
{
    public class TopicLink
    {
        public Guid ProjectId { get; set; }
        public Guid TopicId { get; set; }
        public string Title { get; set; } 
        public string Description { get; set; }        
    }
}

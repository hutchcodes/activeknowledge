using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Common.DTO
{
    public class TopicLink
    {
        public Guid ProjectId { get; set; }
        public Guid TopicId { get; set; }
        public string TopicName { get; set; }
        public string TopicDescription { get; set; }        
    }
}

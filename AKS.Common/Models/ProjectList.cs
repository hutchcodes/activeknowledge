using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Common.Models
{
    public class ProjectList
    {
        public Guid ProjectId { get; set; }
        public string? Name { get; set; }

        public int TopicCount { get; set; } = new Random().Next(25, 75);
    }
}

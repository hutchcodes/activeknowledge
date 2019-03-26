using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Common.DTO
{
    public class Tag
    {
        public Guid ProjectId { get; set; }
        public Guid TagId { get; set; }
        public string TagName { get; set; }
    }
}

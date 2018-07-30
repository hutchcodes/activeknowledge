using System;
using System.Collections.Generic;
using System.Text;

namespace Resurgam.AppCore.Entities
{
    public class Tag : BaseEntity
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
    }
}

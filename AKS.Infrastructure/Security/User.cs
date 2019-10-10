using AKS.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AKS.Infrastructure.Security
{
    public class User 
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }

        public List<Group> Groups { get; set; } = new List<Group>();
    }
}

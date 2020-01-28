using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AKS.Infrastructure.Entities
{
    [Table("Customer")]
    public partial class Customer : BaseEntity
    {
        public Customer()
        {
            Projects = new HashSet<Project>();
            Users = new HashSet<User>();
            Groups = new HashSet<Group>();
        }

        public Guid CustomerId { get; set; }
        public string? Name { get; set; }
        public string? LogoFileName { get; set; }
        public Guid? CustomCssId { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
    }
}

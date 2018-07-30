using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Resurgam.AppCore.Entities
{
    [Table("Customer")]
    public class Customer : BaseEntity
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }

        public string LogoFileName { get; set; }

        public Guid? CustomCssId { get; set; }
    }
}

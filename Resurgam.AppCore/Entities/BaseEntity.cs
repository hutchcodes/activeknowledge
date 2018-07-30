using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Resurgam.AppCore.Entities.Interfaces;

namespace Resurgam.AppCore.Entities
{
    public class BaseEntity : IBaseEntity
    {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
    }
}

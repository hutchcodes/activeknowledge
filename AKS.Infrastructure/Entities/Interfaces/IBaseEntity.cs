using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AKS.Infrastructure.Entities.Interfaces
{
    public interface IBaseEntity
    {
        int Id { get; set; }
    }
}

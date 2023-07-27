using System.ComponentModel.DataAnnotations;

namespace Domain.Core.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public virtual int Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

using Domain.Core.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace Domain.Core.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Blog>? Blogs { get; set; }
    }
}

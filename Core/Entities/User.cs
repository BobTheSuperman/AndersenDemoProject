namespace Domain.Core.Entities
{
    public class User : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public virtual ICollection<Blog>? Blogs { get; set; } = new List<Blog>();
    }
}

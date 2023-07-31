using Domain.Core.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace Domain.Core.Models.Blogs
{
    public class BlogModel
    {
        [Required]
        [MaxLength(30, ErrorMessage = Constants.Validation.Blogs.TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000, ErrorMessage = Constants.Validation.Blogs.TextContentMaxLength)]
        public string Text { get; set; }

        [Required]
        public int AuthorId { get; set; }
    }
}

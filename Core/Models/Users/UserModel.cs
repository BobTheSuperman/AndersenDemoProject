using Domain.Core.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace Domain.Core.Models.Users
{
    public class UserModel
    {
        [Required]
        [StringLength(30, ErrorMessage = Constants.Validation.Users.FirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = Constants.Validation.Users.LastNameMaxLength)]
        public string LastName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = Constants.Validation.Users.EmailMaxLength)]
        [EmailAddress(ErrorMessage = Constants.Validation.Users.EmailError)]
        public string Email { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}

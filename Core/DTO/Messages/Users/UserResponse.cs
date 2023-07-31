using Domain.Core.Entities;

namespace Domain.Core.DTO.Messages.Users
{
    public class UserResponse : BaseResponse
    {
        public User User { get; set; }
    }
}

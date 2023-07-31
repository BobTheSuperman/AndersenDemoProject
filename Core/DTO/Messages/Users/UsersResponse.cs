using Domain.Core.Entities;

namespace Domain.Core.DTO.Messages.Users
{
    public class UsersResponse : BaseResponse
    {
        public IEnumerable<User> Users { get; set; } = new List<User>();
    }
}

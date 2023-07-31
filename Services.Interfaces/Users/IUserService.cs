using Domain.Core.DTO.Messages;
using Domain.Core.DTO.Messages.Users;
using Domain.Core.Models.Users;

namespace Services.Interfaces.Users
{
    public interface IUserService
    {
        Task<UserCreateResponse> CreateUserAsync(UserModel user);

        Task<UserResponse> GetUser(int id);

        Task<BaseResponse> UpdateUser(int id, UserModel user);

        Task<BaseResponse> DeleteUser(int id);

        Task<UsersResponse> GetUsers();
    }
}

using API.DTOs.Users;
using API.Utilities;

namespace API.Interfaces.Users
{
    public interface IUserRepository
    {
        Task<List<UserDto>> GetAllUsersAsync(UserParams usersparams);
    }
}
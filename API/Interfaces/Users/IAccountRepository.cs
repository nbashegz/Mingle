using API.DTOs.Users;
using ErrorOr;

namespace API.Interfaces.Users;

    public interface IAccountRepository
    {
         Task<ErrorOr<LoginDto>> RegisterAsync(RegisteredRequest request);
    }

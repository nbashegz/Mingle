using API.Entities;

namespace API.Interfaces.Services
{
    public interface ITokenService
    {
        Task<string> GenerateAccessTokenAsync(AppUser user);
    }
}
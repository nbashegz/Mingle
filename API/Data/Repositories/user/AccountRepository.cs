using API.DTOs.Users;
using API.Entities;
using API.Interfaces.Services;
using API.Interfaces.Users;
using AutoMapper;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories.user
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        public AccountRepository(UserManager<AppUser> userManager, IMapper mapper, ITokenService tokenService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<ErrorOr<LoginDto>> RegisterAsync(RegisteredRequest request)
        {
            if (await UserNameExists(request.Username!))
                return Error.Conflict("User.DuplicateUsername", $"Username '{request.Username}' is already taken.");
            // var user = new AppUser
            // {

            // };

           var user = _mapper.Map<AppUser>(request);

            var userResult = await _userManager.CreateAsync(user, request.Password);
            if (!userResult.Succeeded)
            {
                var errors = userResult.Errors.Select(e => e.Description).ToArray();
                return Error.Failure("user.IdentityError",string.Join("\n", errors));
            }

            var roleResult = await _userManager.AddToRoleAsync( user, RolePermission.Member);
            if (!roleResult.Succeeded)
            {
                var errors = roleResult.Errors.Select(e => e.Description).ToArray();
                return Error.Failure("user.IdentityError",string.Join("\n", errors));
            }

            return new LoginDto
            {
                Username = user.UserName!,
                Gender = user.Gender,
                Fullname = user.FullName,
                Token = await _tokenService.GenerateAccessTokenAsync(user)
            };
        }

        private async Task<bool> UserNameExists(string username)
        {
            return await _userManager.Users.AnyAsync(u => u.UserName == username);
        }
    }
}
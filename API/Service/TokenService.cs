using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Service;

public class TokenService : ITokenService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _config;

    private readonly SymmetricSecurityKey _key;
    public TokenService(UserManager<AppUser> userManager, IConfiguration config)
    {
        _userManager = userManager;
        _config = config;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
    }
    public async Task<string> GenerateAccessTokenAsync(AppUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        };
        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
        //sign security key
        var signingCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
        //describe how the token will look like after generation.
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = signingCredentials,
            Issuer = _config["Token:Issuer"],
            Audience = _config["Token:Audience"]
        };
        var TokenHandler = new JwtSecurityTokenHandler();
        var token = TokenHandler.CreateToken(tokenDescriptor);

        return TokenHandler.WriteToken(token);
    }
}
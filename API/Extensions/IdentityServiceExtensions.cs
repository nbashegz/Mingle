using System.Text;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {
        services
.AddIdentityCore<AppUser>(options => options.User.RequireUniqueEmail = true)
.AddRoles<AppRole>()
.AddRoleManager<RoleManager<AppRole>>()
.AddSignInManager<SignInManager<AppUser>>()
.AddRoleValidator<RoleValidator<AppRole>>()
.AddEntityFrameworkStores<MingleDBContext>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
                ValidIssuer = config["Token:Issuer"],
                ValidAudience = config["Token:Audience"],
                ClockSkew = TimeSpan.Zero
            };
        });
        return services;
    }
}

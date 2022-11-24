using API.Data;
using API.Data.Repositories.user;
using API.Entities;
using API.Extensions;
using API.Interfaces.Services;
using API.Interfaces.Users;
using API.Service;
using API.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
builder.Services.AddScoped<ITokenService, TokenService>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo{
        Title = "Mingle Api",
        Version = "v1",
        Description = "Mingle API is a Dating Software Application",
        Contact = new OpenApiContact{
            Name = "Eneh-chibuzor Ebube",
            Email = "shegzacademy@gmail.com"
        }
    });
});

//identity

builder.Services.AddIdentityServices(builder.Configuration);
//database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MingleDBContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Mingle API Documentation V1"));
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var userManager = services.GetRequiredService<UserManager<AppUser>>();
var roleManager = services.GetRequiredService<RoleManager<AppRole>>();

await SeedData.SeedUsersAsync(userManager, roleManager);

await app.RunAsync();

using API.DTOs.Users;
using API.Extensions;
using API.Interfaces.Users;
using API.Utilities;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories.user;

public class UserRepository : IUserRepository
{
    private readonly MingleDBContext _dbcontext;
    public UserRepository(MingleDBContext dbcontext)
    {
        _dbcontext = dbcontext;
    }
    public async Task<List<UserDto>> GetAllUsersAsync(UserParams usersparams)
    {
        var query = _dbcontext.Users.AsQueryable();
        query = query.Where(u => u.UserName != usersparams.CurrentUsername);
        query = query.Where(u => u.Gender == usersparams.Gender);

        var minDob = DateTime.Today.AddYears(-usersparams.MaxAge - 1);
        var maxDob = DateTime.Today.AddYears(-usersparams.MinAge);

        query = query.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);
        // switch (usersparams.OrderBy)
        // {
        //     case "created":
        //     query = query.OrderByDescending(u => u.DateCreated);
        //     break;
        //     default:
        //     query = query.OrderByDescending(u => u.LastActive);
        //     break;
        // }
        query = usersparams.OrderBy switch
        {
            "created" => query.OrderByDescending(u => u.DateCreated),
            _ => query.OrderByDescending(u => u.LastActive)
        };
        var userDtos = await query.
        AsNoTracking()
        .Select(a => new UserDto
        {
            Username = a.UserName,
            Fullname = a.FullName,
            MainPhoto = a.Photos!.FirstOrDefault(p => p.IsMain)!.Url,
            Gender = a.Gender,
            Age = a.DateOfBirth.GetAge(),
            LastActive = a.LastActive,
            Created = a.DateCreated
        }
            ).ToListAsync();
        return userDtos;
    }
}
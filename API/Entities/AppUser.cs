using Microsoft.AspNetCore.Identity;

namespace API.Entities;

public class AppUser : IdentityUser<int>
{
public string Firstname { get; set; } = null!;

public string Lastname { get; set; } = null!;

public string Gender {get; set; } = null!;

public string DateOfBirth {get; set;} = null!;

public DateTimeOffset DateCreated {get; set;} = DateTimeOffset.UtcNow;

public DateTimeOffset LastActive {get; set;} = DateTimeOffset.UtcNow;

public string? Interests {get; set;}

public string City {get; set;} = null!;

public string Country {get; set;} = null!;

public string? Introduction {get; set;}

public string? Relationship {get; set;}

public string? Religion {get; set;}

public  ICollection<AppUserRole>?  UserRoles {get; set;}

public ICollection<Photo>? Photos {get; set;}
}
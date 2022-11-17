namespace API.DTOs.Users;

    public class UserDto
    {
        public string? Fullname{get; set;}
        public string? MainPhoto{get; set;}
        public string? Gender {get; set;}
        public int Age {get; set;}
        public DateTimeOffset LastActive{get; set;}
        public DateTimeOffset Created {get; set;}
        public string? Username {get; set;}
    }

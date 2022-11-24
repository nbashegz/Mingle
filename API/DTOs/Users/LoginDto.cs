namespace API.DTOs.Users
{
    public class LoginDto
    {
        public string? MainPhoto {get; set;}
        public string Gender {get; set;} = null!;
        public string Fullname {get; set;} = null!;
        public string Username {get; set;} = null!;
        public string? Token {get; set;}
    }
}
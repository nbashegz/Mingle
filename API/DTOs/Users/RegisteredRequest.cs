using System.ComponentModel.DataAnnotations;
using API.DTOs.Users.Validations;

namespace API.DTOs.Users
{
    public class RegisteredRequest
    {
private string? _firstname;
private string? _lastname;
        [Required(ErrorMessage = "Please Enter Your {0}.")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "{0} should be atleast 3 characters and not more than 10 characters.")]
        public string? Firstname {get => _firstname; set => _firstname = value?.Trim(); }

        [Required(ErrorMessage = "{0} is required.")]
        [MinLength(3, ErrorMessage = "{0} should be atleast 3 characters.")]
        public string? Lastname { get => _lastname; set => _lastname = value?.Trim(); }
        [Required( ErrorMessage = "{0} is required")]
        [Gender(ErrorMessage = "nigga, you should be either 'male' or 'female' LoL")]
        public string? Gender {get; set;}
        [Required(ErrorMessage = "{0} is required.")]
        public DateTime DateOfBirth {get; set;}
        [Required(ErrorMessage = "{0} is required.")]
        [RegularExpression("^[0-9]*$",ErrorMessage = "Mtfer it's not a vald phone number." )]
        public string? Phonenumber {get; set;}
         [Required(ErrorMessage = "{0} is required.")]
        [EmailAddress(ErrorMessage = "{0} is invalid mtfer!")]
        public string? Email {get; set;}
        [Required(ErrorMessage = "{0} is required.")]
        [MinLength(3, ErrorMessage = "Mtfer, {0} should be atleast 3 characters.")]
        public string? Username {get; set;}
        [Required(ErrorMessage = "{0} is required.")]
        [MinLength(3, ErrorMessage = "{0} should be atleast 3 characters LoL.")]
        public string? City {get; set;}
        [Required(ErrorMessage = "{0} is required.")]
        [MinLength(3, ErrorMessage = "{0} should be atleast 3 characters mtfer!")]
        public string? Country {get; set;}
        [Required(ErrorMessage = "{0} is required.")]
        [MinLength(6, ErrorMessage = "{0} should be atleast 6characters mtfer!")]
        public string? Password{get; set;}

        [Required(ErrorMessage = "{0} is required.")]
        [Compare(nameof(Password), ErrorMessage = "passwords do not match, set it up well mtfer!! lol." )]
        public string? ConfirmPassword {get; set;}
    }
}
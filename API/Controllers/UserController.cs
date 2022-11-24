using API.Controllers;
using API.DTOs.Users;
using API.Interfaces.Users;
using API.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace API.controllers;

public class UserController : BaseApiController
{
    private readonly IUserRepository _userRepository;
    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    [HttpGet]
    public async Task<ActionResult<UserDto>> GetAllUsers([FromQuery] UserParams userParams)
    {
        var userDto = await _userRepository.GetAllUsersAsync(userParams);

        return Ok(userDto);
    }
}

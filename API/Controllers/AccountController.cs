using API.DTOs.Users;
using API.Interfaces.Users;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountController : BaseApiController
{
    private readonly IAccountRepository _accountRepository;
    public AccountController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    [HttpPost("register")]
    public async Task<ActionResult<LoginDto>> Register(RegisteredRequest request)
    {
        ErrorOr<LoginDto> response = await _accountRepository.RegisterAsync(request);
        return response.MatchFirst<ActionResult>(
            LoginDto => Ok(LoginDto),
            error => BadRequest(error.Description)
        );
    }
}

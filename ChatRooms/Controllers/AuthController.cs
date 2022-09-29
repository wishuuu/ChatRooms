using ChatRooms.Application.Authorization;
using ChatRooms.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatRooms.Controllers;

public class AuthController : BaseController
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;
    
    public AuthController(IAuthService authService, ITokenService tokenService)
    {
        _authService = authService;
        _tokenService = tokenService;
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] AuthModel request)
    {
        if (!_authService.TryAuthorize(request, out var user))
            return BadRequest("Invalid username or password");

        if (user != null)
        {
            var token = _tokenService.CreateToken(user);
            return Ok(token);
        }

        return StatusCode(500);
    }
}
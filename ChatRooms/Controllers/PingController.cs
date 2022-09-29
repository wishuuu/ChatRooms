using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatRooms.Controllers;

public class PingController : BaseController
{
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public IActionResult Ping()
    {
        return Ok("Pong");
    }
    
    [HttpGet("authorized")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles="Admin")]
    public IActionResult AuthenticatedPing()
    {
        return Ok("Authenticated Pong");
    }
    
}
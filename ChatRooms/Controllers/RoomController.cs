using System.Security.Claims;
using AutoMapper;
using ChatRooms.Application.Authorization;
using ChatRooms.Application.InvitationService;
using ChatRooms.Domain;
using ChatRooms.Domain.SearchCriterias;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatRooms.Controllers;

public class RoomController : BaseController
{
    private readonly IRoomRepository _roomRepository;
    private readonly IMapper _mapper;
    
    public RoomController(IRoomRepository roomRepository, IMapper mapper)
    {
        _roomRepository = roomRepository;
        _mapper = mapper;
    }
    
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Room>), StatusCodes.Status200OK)]
    //[Authorize(Roles = "Admin")]
    // TODO: Uncomment authorization after testing
    public async Task<IActionResult> Get()
    {
        return Ok(await _roomRepository.ListAllAsync());
    }
    
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> Get(int id)
    {
        var room = await _roomRepository.GetByIdAsync(id);
        if (room == null)
        {
            return NotFound();
        }
        
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (AuthFunc.EnsureUserIsInRoom(identity, room, out int _))
        {
            return Unauthorized();
        }

        return Ok(room);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(Room), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<IActionResult> Post([FromBody] Room room)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        //TODO check if room with same name already exists
        //TODO update room with logged user as owner
        //TODO add user to room

        room.Id = await _roomRepository.GetNextIdAsync();
        await _roomRepository.AddAsync(room);
        return CreatedAtAction(nameof(Get), new { id = room.Id }, room);
    }
    
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> Put(int id, [FromBody] Room room)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        if (id != room.Id)
        {
            return BadRequest();
        }

        var existingRoom = await _roomRepository.GetByIdAsync(id);
        if (existingRoom == null)
        {
            return NotFound();
        }
        
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (!AuthFunc.EnsureUserIsRoomOwner(identity, existingRoom, out int _))
        {
            return Unauthorized();
        }

        await _roomRepository.UpdateAsync(room);
        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var room = await _roomRepository.GetByIdAsync(id);
        if (room == null)
        {
            return NotFound();
        }
        
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (!AuthFunc.EnsureUserIsRoomOwner(identity, room, out int _))
        {
            return Unauthorized();
        }

        await _roomRepository.DeleteAsync(room);
        return NoContent();
    }
    
    [HttpGet("search")]
    [ProducesResponseType(typeof(IEnumerable<Room>), StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> Search([FromQuery] RoomSearchCriteria searchCriteria)
    {
        return Ok(await _roomRepository.ListAsync(searchCriteria));
    }

    [HttpGet("invitation")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> GetInvitation([FromQuery] int roomId, [FromQuery] int? userId, [FromServices] IUserRepository userRepository, [FromServices] IRoomInvitationService roomInvitationService)
    {
        var room = await _roomRepository.GetByIdAsync(roomId);
        if (room == null)
        {
            return NotFound();
        }
        
        var user = await userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }
        
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (AuthFunc.EnsureUserIsInRoom(identity, room, out int _))
        {
            return Unauthorized();
        }

        return Ok(roomInvitationService.CreateInvitation(roomId, userId));
    }

}
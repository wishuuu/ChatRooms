using AutoMapper;
using ChatRooms.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ChatRooms.Controllers;

public class MessageController : BaseController
{
    private readonly IRoomRepository _roomRepository;
    private readonly IMapper _mapper;
    
    public MessageController(IRoomRepository roomRepository, IMapper mapper)
    {
        _roomRepository = roomRepository;
        _mapper = mapper;
    }

    [HttpGet("{roomId:int}")]
    [ProducesResponseType(typeof(IEnumerable<Message>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Get(int roomId, int amount, int offset)
    {
        var room = await _roomRepository.GetByIdAsync(roomId);
        if (room == null)
        {
            return NotFound();
        }
        var messages = await _roomRepository.ListMessagesAsync(roomId, amount, offset);
        return Ok(messages);
    }
    
    [HttpPost("{roomId:int}")]
    [ProducesResponseType( StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Send(int roomId, [FromBody] Message message)
    {
        var room = await _roomRepository.GetByIdAsync(roomId);
        if (room == null)
        {
            return NotFound();
        }
        await _roomRepository.AddMessageAsync(roomId, message);
        return CreatedAtAction(nameof(Get), new {roomId, message}, message);
    }
}
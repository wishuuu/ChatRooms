using AutoMapper;
using ChatRooms.Domain;
using ChatRooms.Domain.SearchCriterias;
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
    public async Task<IActionResult> Get()
    {
        return Ok(await _roomRepository.ListAllAsync());
    }
    
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var user = await _roomRepository.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(Room), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] Room user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        user.Id = await _roomRepository.GetNextIdAsync();
        await _roomRepository.AddAsync(user);
        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
    }
    
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(int id, [FromBody] Room user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        if (id != user.Id)
        {
            return BadRequest();
        }

        var existingRoom = await _roomRepository.GetByIdAsync(id);
        if (existingRoom == null)
        {
            return NotFound();
        }

        await _roomRepository.UpdateAsync(user);
        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _roomRepository.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        await _roomRepository.DeleteAsync(user);
        return NoContent();
    }
    
    [HttpGet("search")]
    [ProducesResponseType(typeof(IEnumerable<Room>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Search([FromQuery] RoomSearchCriteria searchCriteria)
    {
        return Ok(await _roomRepository.ListAsync(searchCriteria));
    }
}
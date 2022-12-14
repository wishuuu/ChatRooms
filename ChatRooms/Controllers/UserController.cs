using AutoMapper;
using ChatRooms.Domain;
using ChatRooms.Domain.DTOs;
using ChatRooms.Domain.SearchCriterias;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatRooms.Controllers;

public class UserController : BaseController
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public UserController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
    //[Authorize(Roles = "Admin")]
    // TODO: Uncomment authorization after testing
    public async Task<IActionResult> Get()
    {
        return Ok(await _userRepository.ListAllAsync());
    }
    
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> Get(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [AllowAnonymous]
    public async Task<IActionResult> Post([FromBody] User user)
    {
        // TODO : Add validation if fields are empty
        // TODO : Add validation if email is already used
        // TODO : Add validation if username is already used
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        user.Id = await _userRepository.GetNextIdAsync();
        await _userRepository.AddAsync(user);
        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
    }
    
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> Put(int id, [FromBody] User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        if (id != user.Id)
        {
            return BadRequest();
        }

        var existingUser = await _userRepository.GetByIdAsync(id);
        if (existingUser == null)
        {
            return NotFound();
        }
        
        // TODO : Add validation if user update his own profile

        await _userRepository.UpdateAsync(user);
        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        await _userRepository.DeleteAsync(user);
        return NoContent();
    }
    
    [HttpGet("search")]
    [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> Search([FromQuery] UserSearchCriteria searchCriteria)
    {
        return Ok(await _userRepository.ListAsync(searchCriteria));
    }
}
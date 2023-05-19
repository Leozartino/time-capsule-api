using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TimeCapsule.API.Dtos;

namespace TimeCapsule.API.Controllers;

[ApiController]
[Route("memories")]
public class UserMemoriesController : ControllerBase
{
    private readonly IUserMemoryRepository _userMemoryRepository;
    private readonly IUserRepository _userRepository;

    public UserMemoriesController(IUserMemoryRepository userMemoryRepository, IUserRepository userRepository)
    {
        _userMemoryRepository = userMemoryRepository;
        _userRepository = userRepository;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<UserMemory>>> GetUserMemories()
    {
        var memories = await _userMemoryRepository.GetUserMemories();
        return Ok(memories);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<UserMemory>> GetUserMemory([FromRoute] Guid id)
    {
        var memory = await _userMemoryRepository.GetUserMemory(id);
        
        if(memory is null)
            return NotFound();
        
        return Ok(memory);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<UserMemory>> AddUserMemory([FromBody] UserMemoryToDto memory)
    {

        var user = await _userRepository.GetUser(memory.UserId);

        if (user is null)
            return NotFound("User not found");
        
        UserMemory userMemory = new()
        {
            CoverUrl = memory.CoverUrl,
            Content = memory.Content,
            IsPublic = memory.IsPublic && memory.IsPublic,
            UserId = memory.UserId
        };

        var newMemory = await _userMemoryRepository.AddUserMemory(userMemory);
        return CreatedAtAction(nameof(GetUserMemory), new {id = newMemory.Id}, newMemory);
    }
    
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<UserMemory>> PatchMemory([FromRoute] Guid memoryId)
    {

        var memory = await _userMemoryRepository.GetUserMemory(memoryId);

        if (memory is null)
            return NotFound("Memory not found");
        
        UserMemory userMemory = new()
        {
            CoverUrl = memory.CoverUrl,
            Content = memory.Content,
            IsPublic = memory.IsPublic && memory.IsPublic,
            UserId = memory.UserId
        };

        var newMemory = await _userMemoryRepository.AddUserMemory(userMemory);
        return CreatedAtAction(nameof(GetUserMemory), new {id = newMemory.Id}, newMemory);
    }
}
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace TimeCapsule.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserMemoriesController : ControllerBase
{
    private readonly IUserMemoryRepository _userMemoryRepository;


    public UserMemoriesController(IUserMemoryRepository userMemoryRepository)
    {
        _userMemoryRepository = userMemoryRepository;
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
}
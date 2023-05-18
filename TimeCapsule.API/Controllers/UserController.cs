using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TimeCapsule.API.Dtos;

namespace TimeCapsule.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<User>>> GetUsers()
    {
        var users = await _userRepository.GetUsers();
        return Ok(users);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<User>> GetUser([FromRoute] Guid id)
    {
        var user = await _userRepository.GetUser(id);
        
        if(user is null)
            return NotFound();
        
        return Ok(user);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<User>> AddUser([FromBody] UserToDto userRequest)
    {
        User user = new()
        {
            Name = userRequest.Name,
            PrincipalName = userRequest.PrincipalName,
            GithubId = userRequest.GithubId,
            AvatarUrl = userRequest.AvatarUrl
        };
        
        var newUser = await _userRepository.AddUser(user);
        return CreatedAtAction(nameof(GetUser), new {id = newUser.Id}, newUser);
    }
}
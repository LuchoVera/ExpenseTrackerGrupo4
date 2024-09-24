using Microsoft.AspNetCore.Mvc;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;
using ExpenseTrackerGrupo4.src.Infrastructure.Repositories;

namespace ExpenseTrackerGrupo4.src.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(UserRepository userRepository) : ControllerBase
{
    private readonly IUserRepository _userRepository = userRepository;

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);

        if(user == null)
        {
            return NotFound();
        }

        return Ok();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateUser(User user)
    {
        if(user == null)
        {
            return BadRequest();
        }
        
        await _userRepository.AddUserAsync(user);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] User user)
    {
        if(user == null)
        {
            return BadRequest();
        }
        
        await _userRepository.UpdateUserAsync(user);
        return Ok();
    }

    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);

        if(user == null)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        if(await _userRepository.GetUserByIdAsync(id) == null)
        {
            return NotFound();
        }

        await _userRepository.DeleteUserAsync(id);
        return Ok();
    }
}
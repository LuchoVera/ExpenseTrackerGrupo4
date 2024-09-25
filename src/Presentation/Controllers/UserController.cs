using Microsoft.AspNetCore.Mvc;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;
using ExpenseTrackerGrupo4.src.Infrastructure.Repositories;
using ExpenseTrackerGrupo4.src.Aplication.Interfaces;

namespace ExpenseTrackerGrupo4.src.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        if(user == null)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] User user)
    {
        if(user == null)
        {
            return BadRequest();
        }
        
        await _userService.UpdateUserAsync(user);
        return Ok();
    }

    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var user = await _userService.GetUserByEmailAsync(email);

        if(user == null)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        if(await _userService.GetUserByIdAsync(id) == null)
        {
            return NotFound();
        }

        await _userService.DeleteUserAsync(id);
        return Ok();
    }
}
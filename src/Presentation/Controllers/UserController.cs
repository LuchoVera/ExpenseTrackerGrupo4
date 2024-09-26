using Microsoft.AspNetCore.Mvc;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Aplication.Interfaces;
using Microsoft.AspNetCore.Authorization;
using ExpenseTrackerGrupo4.src.Utils;

namespace ExpenseTrackerGrupo4.src.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        if(user == null)
        {
            return NotFound(new { Message = "User not found." });
        }

        return Ok(user);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] User user)
    {
        if(user == null)
        {
            return BadRequest(new { Message = "Invalid user data." });
        }
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        user.PasswordHash = PasswordHasher.HashPassword(user.PasswordHash);
        await _userService.UpdateUserAsync(user);
        return Ok();
    }

    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var user = await _userService.GetUserByEmailAsync(email);

        if(user == null)
        {
            return NotFound(new { Message = "User not found." });
        }

        return Ok(user);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        if(await _userService.GetUserByIdAsync(id) == null)
        {
            return NotFound(new { Message = "User not found." });
        }

        await _userService.DeleteUserAsync(id);
        return NoContent();
    }
}

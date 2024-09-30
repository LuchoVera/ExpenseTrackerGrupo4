using Microsoft.AspNetCore.Mvc;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Aplication.Interfaces;
using Microsoft.AspNetCore.Authorization;
using ExpenseTrackerGrupo4.src.Utils;
using ExpenseTrackerGrupo4.src.Presentation.DTOs;
using AutoMapper;

namespace ExpenseTrackerGrupo4.src.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController(IUserService userService, IMapper mapper) : ControllerBase
{
    private readonly IUserService _userService = userService;
    private readonly IMapper _mapper = mapper;

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
    public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UserUpdateRequestDTO userUpdated)
    {
        if(userUpdated == null)
        {
            return BadRequest(new { Message = "Invalid user data." });
        }
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var existingUser = await _userService.GetUserByIdAsync(userId);

            if (existingUser == null)
            {
                return NotFound(new { Message = $"User with ID {userId} not found." });
            }

             var userWithSameEmail = await _userService.GetUserByEmailAsync(userUpdated.Email);

            if (userWithSameEmail != null && userWithSameEmail.Id != existingUser.Id)
            {
                return BadRequest(new { Message = "A user with this email address already exists." });
            }

            _mapper.Map(userUpdated, existingUser);
            await _userService.UpdateUserAsync(existingUser);
            
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
        }
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

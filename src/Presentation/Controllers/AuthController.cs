using AutoMapper;
using ExpenseTrackerGrupo4.src.Aplication.Interfaces;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Presentation.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerGrupo4.src.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IMapper _mapper;

    public AuthController(
        IAuthenticationService authenticationService,
        IMapper mapper
    )
    {
        _authenticationService = authenticationService;
        _mapper = mapper;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDTO request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var user = _mapper.Map<User>(request);
            await _authenticationService.RegisterAsync(user);

            return Ok("User registered successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

   [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var token = await _authenticationService.LoginAsync(request.Email, request.Password);
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }

            return Ok(new
            {
                Email = request.Email,
                Token = token
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    
}

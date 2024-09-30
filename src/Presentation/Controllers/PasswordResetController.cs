using Microsoft.AspNetCore.Mvc;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;
using ExpenseTrackerGrupo4.src.Presentation.DTOs;
using ExpenseTrackerGrupo4.src.Utils;
using ExpenseTrackerGrupo4.src.Aplication.Commands.Token;

namespace ExpenseTrackerGrupo4.src.Presentation.Controllers
{
    [ApiController]
    [Route("api/auth/password-reset")]
    public class PasswordResetController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public PasswordResetController(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        [HttpPost("request")]
        public async Task<IActionResult> RequestPasswordReset([FromBody] PasswordResetRequestDto request)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var generateTokenCommand = new GenerateTokenCommand(user, TokenValidatorConstants._resetPasswordSecretKey);
            var token = generateTokenCommand.Execute();

            await _emailService.SendEmailAsync(user.Email, "Reset Password",
                $"Copy this token to reset your password:\n {token}");

            return Ok("Token sended to your email.");
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmPasswordReset([FromBody] PasswordResetConfirmDto confirmRequest)
        {
            var user = await _userRepository.GetUserByEmailAsync(confirmRequest.Email);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var generateTokenCommand = new GenerateTokenCommand(user, TokenValidatorConstants._resetPasswordSecretKey);
            if (!generateTokenCommand.IsTokenValid(confirmRequest.Token))
            {
                return BadRequest("Invalid Token.");
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(confirmRequest.NewPassword);
            await _userRepository.UpdateAsync(user);

            return Ok("Password changed succesfully.");
        }
    }
}

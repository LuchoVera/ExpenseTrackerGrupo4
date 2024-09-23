using System.IdentityModel.Tokens.Jwt;  
using System.Security.Claims;  
using System.Text; 
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Utils;
using Microsoft.IdentityModel.Tokens;

namespace ExpenseTrackerGrupo4.src.Aplication.Commands.Token;

public class GenerateTokenCommand : ICommand<string>
{
    public User User { get; }

    public GenerateTokenCommand(User user)
    {
        User = user;
    }

    public string Execute()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        if(TokenValidatorConstants._secretKey == null ) {
            throw new Exception("No secret key found.");
        }
        var key = Encoding.ASCII.GetBytes(TokenValidatorConstants._secretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.NameIdentifier, User.Id.ToString()),
                    new Claim(ClaimTypes.Email, User.Email)
                ]
            ),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

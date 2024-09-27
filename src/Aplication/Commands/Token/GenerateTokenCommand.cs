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
    private readonly string _secretKey;

     public GenerateTokenCommand(User user, string? secretKey = null)
    {
        User = user;
        _secretKey = secretKey ?? TokenValidatorConstants._secretKey;
    }

    public string Execute()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        if (_secretKey == null)
        {
            throw new Exception("No secret key found.");
        }
        var key = Encoding.ASCII.GetBytes(_secretKey);

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
    public bool IsTokenValid(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var key = Encoding.ASCII.GetBytes(_secretKey);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            return true;
        }
        catch
        {
            return false;
        }
    }

}

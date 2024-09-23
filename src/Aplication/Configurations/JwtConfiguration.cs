using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ExpenseTrackerGrupo4.src.Utils;

namespace ExpenseTrackerGrupo4.Configurations

{
    public static class JwtConfiguration
    {
        public static void AddJwtAuthentication(this IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes(TokenValidatorConstants._secretKey ?? string.Empty);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}

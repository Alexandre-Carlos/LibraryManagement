using LibraryManagement.Application.Configuration;
using LibraryManagement.Core.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryManagement.Application.Services.Authorize
{
    public static class AuthenticationConfig
    {
        public static string GenerateToken(User user, ApplicationConfig appConfig) 
        {
            var symmetricSecurityKey = Encoding.ASCII.GetBytes(appConfig.KeyToken.SymmetricSecurityKey);

            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim("userId", user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricSecurityKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenCreate = tokenHandler.CreateToken(tokenConfig);
            var token = tokenHandler.WriteToken(tokenCreate);

            return token;
        }
    }
}

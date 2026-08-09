using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Users.Application.Interfaces.IToken;
using Users.Domain.Entities;
using Users.Services.Settings;

namespace Users.Services.Token
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly JwtSettings _jwtSettings;
        public TokenGenerator(IOptions<JwtSettings> options) {
            _jwtSettings = options.Value;
        }
        public string GenerateAccessToken(UserEntity user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var claims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.Sub, user.ID.ToString()),
                new (JwtRegisteredClaimNames.PhoneNumber, user.PhoneNumber.Value),
            };
            
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

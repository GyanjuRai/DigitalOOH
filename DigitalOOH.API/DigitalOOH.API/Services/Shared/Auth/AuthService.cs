using DigitalOOH.API.Interfaces.Shared.Auth;
using DigitalOOH.API.Models.Shared.Auth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DigitalOOH.API.Services.Shared.Auth
{
    public class AuthService : IAuthService
    {
        private readonly JwtConfig _jwtConfig;
        private readonly byte[] _secret;

        public AuthService(JwtConfig jwtConfig)
        {
            _jwtConfig = jwtConfig;
            _secret = Encoding.UTF8.GetBytes(_jwtConfig.SecretKey ?? "");
        }

        public JwtResponse GenerateToken(Claim[]? claims)
        {
            bool shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims?.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Aud)?.Value);
            SigningCredentials signingCredentials = new(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,
                audience: shouldAddAudienceClaim ? _jwtConfig.Audience : null,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_jwtConfig.AccessTokenExpirationMin),
                signingCredentials: signingCredentials
            );

            string accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new JwtResponse
            {
                Token = accessToken,
            };
        }
    }
}

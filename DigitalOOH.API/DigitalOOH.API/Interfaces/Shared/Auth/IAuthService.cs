using DigitalOOH.API.Models.Shared.Auth;
using System.Security.Claims;

namespace DigitalOOH.API.Interfaces.Shared.Auth
{
    public interface IAuthService
    {
        public JwtResponse GenerateToken(Claim[]? claims);
    }
}

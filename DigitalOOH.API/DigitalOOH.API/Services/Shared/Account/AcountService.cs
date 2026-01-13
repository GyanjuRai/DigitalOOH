using DigitalOOH.API.DataAccess.DBContext;
using DigitalOOH.API.Interfaces.Shared.Account;
using DigitalOOH.API.Interfaces.Shared.Auth;
using DigitalOOH.API.Models.Shared.Account;
using DigitalOOH.API.Models.Shared.Auth;
using DigitalOOH.API.Models.Shared.Helper;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DigitalOOH.API.Services.Shared.Account
{
    /// <summary>
    /// Provides operations related to account management and services.
    /// </summary>
    /// <remarks>This class serves as the entry point for account-related functionality. Specific operations
    /// and usage details are defined by its members.</remarks>
    public class AcountService : IAccountService
    {
        private readonly DigitalOOHDbContext _context;
        private readonly IAuthService _authService;
        public AcountService(
                DigitalOOHDbContext context,
                IAuthService authService
            )
        {
            _context = context;
            _authService = authService;
        }

        public async Task<UserInfoResponse> Login(UserLoginParam param)
        {
            var user = await _context.Users
                .Where(u => u.Email == param.Email)
                .Select(u => new { u.Id, u.Email, u.PasswordHash})
                .FirstOrDefaultAsync();

            if(user == null)
            {
                return new UserInfoResponse
                {
                    Id = Guid.Empty,
                    Email = null
                };
            }

            if(EncryptDcryptHelper.VerifyPassword(user.PasswordHash, param.Password)) 
            {
                var claim = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email)
                };

                JwtResponse accessToken =_authService.GenerateToken(claim.ToArray());
                return new UserInfoResponse
                {
                    Id = user.Id,
                    Email = user.Email,
                    Token = accessToken.Token,
                    ExpireAt = accessToken.ExpireAt

                };
            }
            else
            {
                return new UserInfoResponse
                {
                    Id = Guid.Empty,
                    Email = string.Empty
                };
            }
        }
    }
}

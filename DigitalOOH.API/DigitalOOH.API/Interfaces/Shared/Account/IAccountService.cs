using DigitalOOH.API.Models.Shared.Account;

namespace DigitalOOH.API.Interfaces.Shared.Account
{
    /// <summary>
    /// Service contract for account authentication operations.
    /// </summary>
    /// <remarks>
    /// Handles user credential validation, JWT token generation, and authentication workflows.
    /// </remarks>
    public interface IAccountService
    {
        /// <summary>
        /// Authenticates a user by email and password credentials.
        /// </summary>
        /// <param name="param">The <see cref="UserLoginParam"/> containing email and password.</param>
        /// <returns>A <see cref="Task{UserInfoResponse}"/> containing user info and JWT token on success, or empty values on failure.</returns>
        public Task<UserInfoResponse> Login(UserLoginParam param);
    }
}

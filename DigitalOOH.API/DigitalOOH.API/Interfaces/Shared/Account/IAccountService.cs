using DigitalOOH.API.Models.Shared.Account;

namespace DigitalOOH.API.Interfaces.Shared.Account
{
    public interface IAccountService
    {
        /// <summary>
        /// Login user with email and password
        /// </summary>
        /// <param name="param">Record with Email and Password</param>
        /// <returns>UserInfoResponse</returns>
        public Task<UserInfoResponse> Login(UserLoginParam param);
    }
}

using DigitalOOH.API.Controllers.Application.Base;
using DigitalOOH.API.Interfaces.Shared.Account;
using DigitalOOH.API.Models.Shared.Account;
using DigitalOOH.API.Models.Shared.Enum;
using DigitalOOH.API.Models.Shared.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace DigitalOOH.API.Controllers.Shared.Account
{
    /// <summary>
    /// Account Controller - Handles user authentication and login operations.
    /// </summary>
    /// <remarks>
    /// Provides the login endpoint for user authentication. The login endpoint is publicly accessible
    /// (no authentication required) to allow users to obtain JWT tokens for subsequent requests.
    /// </remarks>
    public class AccountController : BaseController
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance for logging account actions.</param>
        /// <param name="accountService">The account service dependency for handling authentication.</param>
        public AccountController(
                ILogger<AccountController> logger,
                IAccountService accountService
            )
        {
            _logger = logger;
            _accountService = accountService;
        }

        /// <summary>
        /// Authenticates a user with email and password credentials.
        /// </summary>
        /// <remarks>
        /// Validates email format, verifies credentials, and returns a JWT token on success.
        /// Returns 401 Unauthorized for invalid credentials, 200 OK for other error cases (invalid email, user not found).
        /// </remarks>
        /// <param name="param">The <see cref="UserLoginParam"/> containing email and password.</param>
        /// <returns>
        /// HTTP 200 OK with <see cref="LoginResponse"/> (JWT token) on successful authentication.
        /// HTTP 200 OK with error response for invalid email or user not found.
        /// HTTP 401 Unauthorized with InvalidCredential response if credentials do not match.
        /// </returns>
        /// <response code="200">Authentication completed (success, invalid email, or user not found)</response>
        /// <response code="401">Authentication failed (invalid credentials)</response>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginParam param)
        {
            _logger.LogInformation("=============================> POST: Login");

            if (!MailAddress.TryCreate(param.Email, out _))
            {
                return Ok(new ResponseModel<object>
                {
                    Type = ResponseEnum.InvalidCredential.ToString(),
                    Message = "Invalid email address",
                    Data = null
                });
            }

            UserInfoResponse response = await _accountService.Login(param);

            if (response.Email == null)
            {
                return Ok(new ResponseModel<object> 
                {
                    Type = ResponseEnum.NoRecordFound.ToString(),
                    Message = "No user found", 
                    Data = null 
                });
            }

            if (response.Id == Guid.Empty && response.Email == String.Empty)
            {
                return Unauthorized(new ResponseModel<object> 
                { 
                    Type = ResponseEnum.InvalidCredential.ToString(), 
                    Message = "Invalid credential", 
                    Data = null
                });
            }
            else
            {
                LoginResponse res = new LoginResponse
                {
                    Token = response.Token,
                };
                return Ok(new ResponseModel<LoginResponse> 
                {
                    Type = ResponseEnum.Sucess.ToString(),
                    Message = "Login sucess",
                    Data = res
                });
            }
        }

    }
}

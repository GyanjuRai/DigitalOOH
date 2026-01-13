

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
    public class AccountController : BaseController
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;
        public AccountController(
                ILogger<AccountController> logger,
                IAccountService accountService
            )
        {
            _logger = logger;
            _accountService = accountService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginParam param)
        {
            _logger.LogInformation("=============================> POST: Login");

            if (!MailAddress.TryCreate(param.Email, out _))
            {
                return BadRequest(new ResponseModel<object>
                {
                    Type = ResponseEnum.InvalidCredential.ToString(),
                    Message = "Invalid email address",
                    Data = null
                });
            }

            UserInfoResponse response = await _accountService.Login(param);

            if (response.Email == null)
            {
                return NotFound(new ResponseModel<object> 
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
                    Type = ResponseEnum.Unauthorized.ToString(), 
                    Message = "Invalid credential", 
                    Data = null
                });
            }
            else
            {
                return Ok(new ResponseModel<UserInfoResponse> 
                {
                    Type = ResponseEnum.Sucess.ToString(),
                    Message = "Login sucess",
                    Data = response
                });
            }
        }

    }
}

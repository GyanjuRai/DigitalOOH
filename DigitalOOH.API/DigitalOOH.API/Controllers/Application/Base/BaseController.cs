using DigitalOOH.API.Const;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DigitalOOH.API.Controllers.Application.Base
{
    [Produces("application/json")]
    [EnableCors(AppData.PolicyName)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
    }
}

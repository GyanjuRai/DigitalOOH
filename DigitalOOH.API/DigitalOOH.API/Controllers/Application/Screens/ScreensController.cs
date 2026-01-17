using DigitalOOH.API.Controllers.Application.Base;
using DigitalOOH.API.Interfaces.Application.Screens;
using DigitalOOH.API.Models.Application;
using DigitalOOH.API.Models.Shared.Enum;
using DigitalOOH.API.Models.Shared.Response;
using Microsoft.AspNetCore.Mvc;

namespace DigitalOOH.API.Controllers.Application.Screens
{
    public class ScreensController : BaseController
    {
        private readonly IScreeensService _screenService;
        private readonly ILogger<ScreensController> _logger;
        public ScreensController(
            IScreeensService screenService,
            ILogger<ScreensController> logger
            ) 
        {
            _screenService = screenService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetScreens()
        {
            _logger.LogInformation("========================> GET: GetScreens");
            
            var response = await _screenService.GetScreens();

            return Ok(new ResponseModel<GridResponse<ScreensModel>>
            {
                Type = ResponseEnum.Sucess.ToString(),
                Message = response.TotalRows == 0 
                ? "No screens found" 
                : "Screens retrived successfully",
                Data = response
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetScreensForDropdown()
        {
            _logger.LogInformation("========================> GET: GetScreensForDropdown");

            var response = await _screenService.GetScreenNameAndId();

            return Ok(new ResponseModel<List<ScreenNameAndId>>
            {
                Type = ResponseEnum.Sucess.ToString(),
                Message = response.Count == 0 
                ? "No screens found"
                : "Screens retrived sucessfully",
                Data = response
            });
        }

        [HttpPost]
        public async Task<IActionResult> ScreenAdd([FromBody]ScreenParam param)
        {
            _logger.LogInformation("======================> POST: ScreenAdd");

            var response = await _screenService.ScreenAdd(param);

            return Ok(new ResponseModel<ScreensModel> 
            { 
                Type = ResponseEnum.Sucess.ToString(),
                Message = "Screen added",
                Data = response
            });
        }

        [HttpPut]
        public async Task<IActionResult> ScreenEdit([FromBody]ScreenEditParam param)
        {
            _logger.LogInformation("=====================> PUT: ScreenEdit");

            var response = await _screenService.ScreenEdit(param);

            if(response == null)
            {
                return NotFound(new ResponseModel<object>
                {
                    Type = ResponseEnum.NoRecordFound.ToString(),
                    Message = "Screen not found"
                });
            }

            return Ok(new ResponseModel<ScreensModel?>
            {
                Type = ResponseEnum.Sucess.ToString(),
                Message = "Screen updated",
                Data = response
            });
        }
    }
}

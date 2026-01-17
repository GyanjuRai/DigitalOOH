using DigitalOOH.API.Controllers.Application.Base;
using DigitalOOH.API.Interfaces.Shared.Media;
using DigitalOOH.API.Models.Shared.Enum;
using DigitalOOH.API.Models.Shared.Media;
using DigitalOOH.API.Models.Shared.Response;
using Microsoft.AspNetCore.Mvc;

namespace DigitalOOH.API.Controllers.Shared.Media
{
    public class MediaController : BaseController
    {
        private readonly ILogger<MediaController> _logger;
        private readonly IMediaService _mediaService;

        public MediaController(
            ILogger<MediaController> logger,
            IMediaService mediaService
            )
        {
            _logger = logger;
            _mediaService = mediaService;
        }

        [HttpPost]
        [RequestSizeLimit(10_000_000)]
        public async Task<IActionResult> UploadMedia([FromForm] MediaUploadParam param)
        {
            if (param.File == null || param.File.Length == 0) return BadRequest("File is required");

            var result = await _mediaService.UploadAsync(param);

            return Ok(new ResponseModel<object>
            {
                Type = ResponseEnum.Sucess.ToString(),
                Message = "File upload",
                Data = result
            });
        }

    }
}

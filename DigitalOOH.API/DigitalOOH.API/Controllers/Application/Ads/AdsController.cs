using DigitalOOH.API.Controllers.Application.Base;
using DigitalOOH.API.Interfaces.Application.Ads;
using DigitalOOH.API.Models.Application;
using DigitalOOH.API.Models.Shared.Enum;
using DigitalOOH.API.Models.Shared.Response;
using Microsoft.AspNetCore.Mvc;

namespace DigitalOOH.API.Controllers.Application.Ads
{
    public class AdsController : BaseController
    {
        private readonly IAdsService _adsService;
        private readonly ILogger<AdsController> _logger;
        public AdsController(
            IAdsService adService,
            ILogger<AdsController> logger
            ) 
        { 
            _adsService = adService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAds()
        {
            _logger.LogInformation("======================> GET: GetAds");

            var ads = await _adsService.GetAds();

            return Ok(new ResponseModel<GridResponse<AdsModel>> 
            { 
                Type = ResponseEnum.Sucess.ToString(),
                Message = ads?.Data?.Count > 0 
                ? "Ads data get sucessfully"
                : "No ads found",
                Data = ads
            });
        }

        [HttpPost]
        public async Task<IActionResult> GetAdsForDropdown()
        {
            _logger.LogInformation("=====================> GET: GetAdsForDropdown");

            var ads = await _adsService.GetAdsNamesAndId();

            return Ok(new ResponseModel<List<AdsNameAndId>>
            {
                Type = ResponseEnum.Sucess.ToString(),
                Message = ads.Count > 0 
                ? "Ads data get sucessfully"
                : "No ads found",
                Data = ads
            });
        }

        [HttpPost]
        public async Task<IActionResult> AdAdd([FromBody]AdCreateParam param)
        {
            _logger.LogInformation("====================> POST: AdsAdd");

            var ad = await _adsService.AdsAdd(param);

            return Ok(new ResponseModel<AdsModel>
            {
                Type = ResponseEnum.Sucess.ToString(),
                Message = "Ad added",
                Data = ad
            });
        }

        [HttpDelete]
        public async Task<IActionResult> AdRemove(AdsIdParam param)
        {
            _logger.LogInformation("==================> DELETE: AdRemove");

            var deleted = await _adsService.AdsRemove(param);

            return Ok(new ResponseModel<bool>
            {
                Type = deleted ? ResponseEnum.Sucess.ToString() : ResponseEnum.Failed.ToString(),
                Message = deleted ? "Ad deleted" : "Ad delete failed",
                Data = deleted
            });
        }   
    }
}

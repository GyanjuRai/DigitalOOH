using DigitalOOH.API.Controllers.Application.Base;
using DigitalOOH.API.Interfaces.Application.Ads;
using DigitalOOH.API.Models.Application;
using DigitalOOH.API.Models.Shared.Enum;
using DigitalOOH.API.Models.Shared.Response;
using Microsoft.AspNetCore.Mvc;

namespace DigitalOOH.API.Controllers.Application.Ads
{
    /// <summary>
    /// Ads Management Controller - Handles all ad-related HTTP operations.
    /// </summary>
    /// <remarks>
    /// Provides RESTful endpoints for managing advertisements including retrieval, creation, and deletion.
    /// All responses follow the standardized <see cref="ResponseModel{T}"/> format.
    /// </remarks>
    public class AdsMngController : BaseController
    {
        private readonly IAdsService _adsService;
        private readonly ILogger<AdsMngController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdsMngController"/> class.
        /// </summary>
        /// <param name="adService">The <see cref="IAdsService"/> for ad operations.</param>
        /// <param name="logger">The logger instance for tracking controller actions.</param>
        public AdsMngController(
            IAdsService adService,
            ILogger<AdsMngController> logger
            )
        {
            _adsService = adService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all ads with detailed information in a grid format.
        /// </summary>
        /// <returns>
        /// HTTP 200 OK with <see cref="ResponseModel{GridResponse{AdsModel}}"/> containing all ads.
        /// </returns>
        /// <response code="200">Ads retrieved successfully or no ads found</response>
        [HttpGet]
        public async Task<IActionResult> GetAds()
        {
            _logger.LogInformation("======================> GET: GetAds");

            var ads = await _adsService.GetAds();

            return Ok(new ResponseModel<GridResponse<AdsModel>>
            {
                Type = ads?.Data?.Count > 0
                    ? ResponseEnum.Sucess.ToString()
                    : ResponseEnum.NoRecordFound.ToString(),

                Message = ads?.Data?.Count > 0
                    ? "Ads data retrieved successfully"
                    : "No ads found",

                Data = ads
            });
        }

        /// <summary>
        /// Retrieves a lightweight list of ad IDs and titles for dropdown controls.
        /// </summary>
        /// <returns>
        /// HTTP 200 OK with <see cref="ResponseModel{T}"/> containing a <see cref="List{AdsNameAndId}"/> of ad names and IDs.
        /// </returns>
        /// <response code="200">Ad list retrieved successfully or no ads found</response>
        [HttpGet]
        public async Task<IActionResult> GetAdsForDropdown()
        {
            _logger.LogInformation("=====================> GET: GetAdsForDropdown");

            var ads = await _adsService.GetAdsNamesAndId();

            return Ok(new ResponseModel<List<AdsNameAndId>>
            {
                Type = ads.Count > 0 
                    ? ResponseEnum.Sucess.ToString()
                    : ResponseEnum.NoRecordFound.ToString(),

                Message = ads.Count > 0
                    ? "Ads data retrieved successfully"
                    : "No ads found",
                
                Data = ads
            });
    }

        /// <summary>
        /// Creates a new ad with associated media file upload.
        /// </summary>
        /// <param name="param">The <see cref="AdCreateParam"/> containing ad details (Title, MediaType, DurationSeconds, File).</param>
        /// <returns>
        /// HTTP 200 OK with <see cref="ResponseModel{AdsModel}"/> containing the created ad.
        /// </returns>
        /// <response code="200">Ad created successfully</response>
        [HttpPost]
        public async Task<IActionResult> AdAdd([FromForm] AdCreateParam param)
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

        /// <summary>
        /// Deletes an ad and its associated media file from the system.
        /// </summary>
        /// <remarks>
        /// Prevents deletion if ad is assigned to active or future campaigns.
        /// </remarks>
        /// <param name="param">The <see cref="AdsIdParam"/> route parameter containing the ad ID to delete.</param>
        /// <returns>
        /// HTTP 200 OK with <see cref="ResponseModel{bool}"/> indicating deletion status.
        /// </returns>
        /// <response code="200">Ad deleted successfully</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> AdRemove([FromRoute]AdsIdParam param)
        {
            _logger.LogInformation("==================> DELETE: AdRemove");

            var deleted = await _adsService.AdsRemove(param);

            return Ok(new ResponseModel<bool>
            {
                    Type = ResponseEnum.Sucess.ToString(),
                    Message = "Ad deleted",
                    Data = deleted
            });
        }
     
    }   
}


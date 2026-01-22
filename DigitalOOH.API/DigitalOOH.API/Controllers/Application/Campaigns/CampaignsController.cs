using DigitalOOH.API.Controllers.Application.Base;
using DigitalOOH.API.Interfaces.Application.Campaigns;
using DigitalOOH.API.Models.Application;
using DigitalOOH.API.Models.Shared.Enum;
using DigitalOOH.API.Models.Shared.Response;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DigitalOOH.API.Controllers.Application.Campaigns
{
    /// <summary>
    /// Campaigns Management Controller - Handles all campaign-related HTTP operations.
    /// </summary>
    /// <remarks>
    /// Provides RESTful endpoints for managing advertising campaigns with multi-screen and multi-ad scheduling.
     /// All responses follow the standardized <see cref="ResponseModel{T}"/> format.
    /// </remarks>
    public class CampaignsController : BaseController
    {
        private readonly ILogger<CampaignsController> _logger;
        private readonly ICampaignsService _campaignsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CampaignsController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance for tracking controller actions.</param>
        /// <param name="campaignsService">The <see cref="ICampaignsService"/> for campaign operations.</param>
        public CampaignsController(
            ILogger<CampaignsController> logger,
            ICampaignsService campaignsService
            )
        {
            _logger = logger;
            _campaignsService = campaignsService;
        }

        /// <summary>
        /// Retrieves all advertising campaigns with scheduling and resource information.
        /// </summary>
        /// <returns>
        /// HTTP 200 OK with <see cref="ResponseModel{GridResponse{CampaignsModel}}"/> containing all campaigns.
        /// </returns>
        /// <response code="200">Campaigns retrieved successfully or no campaigns found</response>
        [HttpGet]
        public async Task<IActionResult> GetCampaigns()
        {
            _logger.LogInformation("======================> GET: GetCampaigns");

            var campaigns = await _campaignsService.GetCampaigns();
            
            return Ok(new ResponseModel<GridResponse<CampaignsModel>>
            {
                Type = campaigns?.Data?.Count > 0
                ? ResponseEnum.Sucess.ToString()
                : ResponseEnum.NoRecordFound.ToString(),

                Message = campaigns?.Data?.Count > 0
                    ? "Campaigns data retrieved successfully"
                    : "No campaigns found",

                Data = campaigns
            });
        }

        /// <summary>
        /// Creates a new advertising campaign with associated screens and ads.
        /// </summary>
        /// <param name="param">The <see cref="CampaignsCreateParam"/> containing campaign name, dates, screens list, and ads with play order.</param>
        /// <returns>
        /// HTTP 200 OK with <see cref="ResponseModel{CampaignsModel}"/> containing the created campaign.
        /// </returns>
        /// <response code="200">Campaign created successfully</response>
        [HttpPost]
        public async Task<IActionResult> AddCampaign([FromBody] CampaignsCreateParam param)
        {
            _logger.LogInformation("====================> POST: AddCampaign");
           
            var campaign = await _campaignsService.AddCampaign(param);
            
            return Ok(new ResponseModel<CampaignsModel>
            {
                Type = ResponseEnum.Sucess.ToString(),
               Message = "Campaign added successfully",
                Data = campaign
            });
        }
    }
}

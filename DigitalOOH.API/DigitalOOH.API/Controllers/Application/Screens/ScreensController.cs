using DigitalOOH.API.Controllers.Application.Base;
using DigitalOOH.API.Interfaces.Application.Screens;
using DigitalOOH.API.Models.Application;
using DigitalOOH.API.Models.Shared.Enum;
using DigitalOOH.API.Models.Shared.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalOOH.API.Controllers.Application.Screens
{
    /// <summary>
    /// Screens Management Controller - Handles all screen-related HTTP operations.
    /// </summary>
    /// <remarks>
    /// Provides RESTful endpoints for managing screens (display devices) and retrieving playlists for playback.
    /// All responses follow the standardized <see cref="ResponseModel{T}"/> format.
    /// </remarks>
    public class ScreensController : BaseController
    {
        private readonly IScreeensService _screenService;
        private readonly ILogger<ScreensController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreensController"/> class.
        /// </summary>
        /// <param name="screenService">The <see cref="IScreeensService"/> for screen operations.</param>
        /// <param name="logger">The logger instance for tracking controller actions.</param>
        public ScreensController(
            IScreeensService screenService,
            ILogger<ScreensController> logger
            ) 
        {
            _screenService = screenService;
            _logger = logger;
        }

        #region Screen CRUD
        /// <summary>
        /// Retrieves all screens with detailed information in a grid format.
        /// </summary>
        /// <returns>
        /// HTTP 200 OK with <see cref="ResponseModel{GridResponse{ScreensModel}}"/> containing all screens.
        /// </returns>
        /// <response code="200">Screens retrieved successfully or no screens found</response>
        [HttpGet]
        public async Task<IActionResult> GetScreens()
        {
            _logger.LogInformation("========================> GET: GetScreens");
            
            var response = await _screenService.GetScreens();

            return Ok(new ResponseModel<GridResponse<ScreensModel>>
            {
                Type = response.TotalRows == 0 
                    ? ResponseEnum.NoRecordFound.ToString()
                    : ResponseEnum.Sucess.ToString(),
                Message = response.TotalRows == 0 
                    ? "No screens found" 
                    : "Screens retrieved successfully",
                Data = response
            });
        }

        /// <summary>
        /// Retrieves a lightweight list of screen IDs and names for dropdown controls.
        /// </summary>
        /// <returns>
        /// HTTP 200 OK with <see cref="ResponseModel{List{ScreenNameAndId}}"/> containing screen names and IDs.
        /// </returns>
        /// <response code="200">Screen list retrieved successfully or no screens found</response>
        [HttpGet]
        public async Task<IActionResult> GetScreensForDropdown()
        {
            _logger.LogInformation("========================> GET: GetScreensForDropdown");

            var response = await _screenService.GetScreenNameAndId();

            return Ok(new ResponseModel<List<ScreenNameAndId>>
            {
                Type = response.Count == 0 
                    ? ResponseEnum.NoRecordFound.ToString()
                    : ResponseEnum.Sucess.ToString(),
                Message = response.Count == 0 
                    ? "No screens found"
                    : "Screens retrieved successfully",
                Data = response
            });
        }

        /// <summary>
        /// Creates a new screen with the provided details.
        /// </summary>
        /// <param name="param">The <see cref="ScreenParam"/> containing screen information (Name, Location, Resolution, IsActive).</param>
        /// <returns>
        /// HTTP 200 OK with <see cref="ResponseModel{ScreensModel}"/> containing the created screen.
        /// </returns>
        /// <response code="200">Screen created successfully</response>
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

        /// <summary>
        /// Updates an existing screen with the provided details.
        /// </summary>
        /// <param name="param">The <see cref="ScreenEditParam"/> containing screen ID and updated information.</param>
        /// <returns>
        /// HTTP 200 OK with updated <see cref="ScreensModel"/>, or NoRecordFound if screen doesn't exist.
        /// </returns>
        /// <response code="200">Screen updated successfully or not found</response>
        [HttpPut]
        public async Task<IActionResult> ScreenEdit([FromBody]ScreenEditParam param)
        {
            _logger.LogInformation("=====================> PUT: ScreenEdit");

            var response = await _screenService.ScreenEdit(param);

            if(response == null)
            {
                return Ok(new ResponseModel<object>
                {
                    Type = ResponseEnum.NoRecordFound.ToString(),
                    Message = "Screen not found"
                });
            }

            return Ok(new ResponseModel<ScreensModel>
            {
                Type = ResponseEnum.Sucess.ToString(),
                Message = "Screen updated",
                Data = response
            });
        }
        #endregion

        #region Playlist
        /// <summary>
        /// Retrieves the active ad playlist for a specific screen at the requested time.
        /// </summary>
        /// <remarks>
        /// This endpoint is publicly accessible (AllowAnonymous) to allow screens to retrieve their playlists.
        /// Logs proof of play data for campaign tracking and analytics.
        /// </remarks>
        /// <param name="ScreenId">The GUID of the screen requesting the playlist.</param>
        /// <param name="param">The <see cref="PlayListItemRequest"/> containing the requested playback time.</param>
        /// <returns>
        /// HTTP 200 OK with <see cref="ResponseModel{List{PlayListItem}}"/> containing ads to play.
        /// Returns NoRecordFound if no active campaigns or ads are available.
        /// </returns>
        /// <response code="200">Playlist retrieved successfully or no ads available</response>
        [AllowAnonymous]
        [HttpGet("{ScreenId}/playlist")]
        public async Task<IActionResult> GetPlaylist([FromRoute] Guid ScreenId, [FromQuery] PlayListItemRequest param)
        {
            _logger.LogInformation("======================> GET: GetPlaylist");

            var playlistResponse = await _screenService.GetPlaylist(ScreenId, param);

            if (!playlistResponse.Ads.Any())
            {
                return Ok(new ResponseModel<PlayListItem>
                {
                    Type = ResponseEnum.NoRecordFound.ToString(),
                    Message = "No ads available to play",
                    Data = null
                });
            }

            var proofOfPlay = new ProofOfPlayRequest
            {
                ScreenId = ScreenId,
                CampaignId = playlistResponse.CampaignId,
                playList = playlistResponse.Ads,
                StartAt = param.At
            };

            await _screenService.LogProofOfPlay(proofOfPlay);

            return Ok(new ResponseModel<List<PlayListItem>>
            {
                Type = ResponseEnum.Sucess.ToString(),
                Message = "Playlist retrieved successfully",
                Data = playlistResponse.Ads.Select(a => new PlayListItem
                {
                    AdId = a.AdId,
                    DurationSeconds = a.DurationSeconds,
                    MediaUrl = a.MediaUrl
                }).ToList()
            });
        }
        #endregion
    }
}

using DigitalOOH.API.Models.Application;
using DigitalOOH.API.Models.Shared.Response;

namespace DigitalOOH.API.Interfaces.Application.Campaigns
{
    /// <summary>
    /// Service contract for Campaign operations in the Digital OOH system.
    /// </summary>
    /// <remarks>
    /// Manages the complete lifecycle of advertising campaigns including creation, retrieval, scheduling, and screen assignment.
    /// Campaigns orchestrate ad display across multiple screens during specified time windows.
    /// </remarks>
    public interface ICampaignsService
    {
        /// <summary>
        /// Retrieves all campaigns with detailed information in a grid format.
        /// </summary>
        /// <returns>
        /// A <see cref="Task{GridResponse{CampaignsModel}}"/> containing all campaigns with total row count, schedules, screens, and ads.
        /// </returns>
        public Task<GridResponse<CampaignsModel>> GetCampaigns();

        /// <summary>
        /// Creates a new advertising campaign with associated screens and ads.
        /// </summary>
        /// <remarks>
        /// Validates all parameters, verifies screen and ad existence, creates relationships, and persists to database.
        /// </remarks>
        /// <param name="param">
        /// The <see cref="CampaignsCreateParam"/> containing campaign name, dates, screens list, and ads with play order.
        /// </param>
        /// <returns>
        /// A <see cref="Task{CampaignsModel}"/> containing the created campaign with generated ID and timestamps.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when validation fails (empty name, invalid dates, empty lists).
        /// </exception>
        /// <exception cref="KeyNotFoundException">
        /// Thrown when referenced screens or ads do not exist.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown when business rules are violated (inactive screens, unavailable ads).
        /// </exception>
        public Task<CampaignsModel> AddCampaign(CampaignsCreateParam param);
    }
}

using DigitalOOH.API.Models.Application;
using DigitalOOH.API.Models.Shared.Response;

namespace DigitalOOH.API.Interfaces.Application.Screens
{
    /// <summary>
    /// Service contract for Screen operations in the Digital OOH system.
    /// </summary>
    /// <remarks>
    /// Manages the complete lifecycle of screens including creation, retrieval, updates, and playlist management.
    /// Screens are physical display devices that play ads as part of campaigns.
    /// </remarks>
    public interface IScreeensService
    {
        /// <summary>
        /// Retrieves all available screens in a paginated grid format.
        /// </summary>
        /// <returns>A <see cref="Task{GridResponse{ScreensModel}}"/> containing all screens with total row count.</returns>
        public Task<GridResponse<ScreensModel>> GetScreens();

        /// <summary>
        /// Retrieves screen names and IDs optimized for dropdown controls.
        /// </summary>
        /// <returns>A <see cref="Task{List{ScreenNameAndId}}"/> containing screen ID and name pairs.</returns>
        public Task<List<ScreenNameAndId>> GetScreenNameAndId();

        /// <summary>
        /// Creates a new screen with the specified parameters.
        /// </summary>
        /// <param name="param">The <see cref="ScreenParam"/> containing screen details (Name, Location, Resolution, IsActive).</param>
        /// <returns>A <see cref="Task{ScreensModel}"/> containing the created screen with generated ID and timestamps.</returns>
        public Task<ScreensModel> ScreenAdd(ScreenParam param);

        /// <summary>
        /// Updates an existing screen with the provided parameters.
        /// </summary>
        /// <param name="param">The <see cref="ScreenEditParam"/> containing screen ID and updated details.</param>
        /// <returns>A <see cref="Task{ScreensModel}"/> containing the updated screen, or null if screen not found.</returns>
        public Task<ScreensModel?> ScreenEdit(ScreenEditParam param);

        /// <summary>
        /// Retrieves the playlist of ads to be played on a specific screen at a given time.
        /// </summary>
        /// <param name="ScreenId">The GUID of the screen requesting the playlist.</param>
        /// <param name="param">The <see cref="PlayListItemRequest"/> containing the requested time.</param>
        /// <returns>A <see cref="Task{PlayListResponse}"/> containing the campaign ID and list of ads to play.</returns>
        public Task<PlayListResponse> GetPlaylist(Guid ScreenId, PlayListItemRequest param);

        /// <summary>
        /// Logs proof of play data for campaign tracking and analytics.
        /// </summary>
        /// <param name="param">The <see cref="ProofOfPlayRequest"/> containing screen ID, campaign ID, ads played, and timestamp.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task LogProofOfPlay(ProofOfPlayRequest param);
    }
}

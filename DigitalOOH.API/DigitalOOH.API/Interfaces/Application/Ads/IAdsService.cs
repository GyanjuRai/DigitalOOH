using DigitalOOH.API.Models.Application;
using DigitalOOH.API.Models.Shared.Response;

namespace DigitalOOH.API.Interfaces.Application.Ads
{
    /// <summary>
    /// Service contract for Advertisement operations in the Digital OOH system.
    /// </summary>
    /// <remarks>
    /// Manages the complete lifecycle of advertisements including creation, retrieval, and deletion.
    /// Handles media file upload/storage coordination and campaign reference validation.
    /// </remarks>
    public interface IAdsService
    {
        /// <summary>
        /// Retrieves all advertisements in a paginated grid format.
        /// </summary>
        /// <returns>A <see cref="Task{GridResponse{AdsModel}}"/> containing all ads with total row count.</returns>
        public Task<GridResponse<AdsModel>> GetAds();

        /// <summary>
        /// Retrieves ad names and IDs optimized for dropdown controls.
        /// </summary>
        /// <returns>A <see cref="Task{List{AdsNameAndId}}"/> containing ad ID and name pairs.</returns>
        public Task<List<AdsNameAndId>> GetAdsNamesAndId();

        /// <summary>
        /// Creates a new advertisement with associated media file upload.
        /// </summary>
        /// <param name="param">The <see cref="AdCreateParam"/> containing ad details (Title, MediaType, DurationSeconds, File).</param>
        /// <returns>A <see cref="Task{AdsModel}"/> containing the created ad with generated ID and media URL.</returns>
        /// <exception cref="InvalidOperationException">Thrown if media upload or file validation fails.</exception>
        public Task<AdsModel> AdsAdd(AdCreateParam param);

        /// <summary>
        /// Removes an advertisement and its associated media file from the system.
        /// </summary>
        /// <remarks>
        /// Prevents deletion if the ad is currently assigned to campaigns.
        /// </remarks>
        /// <param name="param">The <see cref="AdsIdParam"/> containing the ad ID to delete.</param>
        /// <returns>A <see cref="Task{bool}"/> indicating if deletion was successful.</returns>
        /// <exception cref="InvalidOperationException">Thrown if ad is in use by active campaigns.</exception>
        /// <exception cref="KeyNotFoundException">Thrown if ad is not found.</exception>
        public Task<bool> AdsRemove(AdsIdParam param);
    }
}

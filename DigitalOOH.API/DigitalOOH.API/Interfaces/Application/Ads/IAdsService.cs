using DigitalOOH.API.Models.Application;
using DigitalOOH.API.Models.Shared.Response;

namespace DigitalOOH.API.Interfaces.Application.Ads
{
    public interface IAdsService
    {
        /// <summary>
        /// List all the ads
        /// </summary>
        /// <returns></returns>
        public Task<GridResponse<AdsModel>> GetAds();
        /// <summary>
        /// Get the ads name and id for dropdown
        /// </summary>
        /// <returns></returns>
        public Task<List<AdsNameAndId>> GetAdsNamesAndId();
        /// <summary>
        /// Insert ad
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Task<AdsModel> AdsAdd(AdCreateParam param);
        /// <summary>
        /// Remove ad
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Task<bool> AdsRemove(AdsIdParam param);
    }
}

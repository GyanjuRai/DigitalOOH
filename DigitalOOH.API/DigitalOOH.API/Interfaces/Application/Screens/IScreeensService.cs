using DigitalOOH.API.Models.Application;
using DigitalOOH.API.Models.Shared.Response;

namespace DigitalOOH.API.Interfaces.Application.Screens
{
    public interface IScreeensService
    {
        /// <summary>
        /// List all the available screens
        /// </summary>
        /// <returns></returns>
        public Task<GridResponse<ScreensModel>> GetScreens();
        /// <summary>
        /// List all available screens name and id for dropdown
        /// </summary>
        /// <returns></returns>
        public Task<List<ScreenNameAndId>> GetScreenNameAndId();
        /// <summary>
        /// Add new screen
        /// </summary>
        /// <returns></returns>
        public Task<ScreensModel> ScreenAdd(ScreenParam param);
        /// <summary>
        /// Edit existing screen with the passed id
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Task<ScreensModel?> ScreenEdit(ScreenEditParam param);
    }
}

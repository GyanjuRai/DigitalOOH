using DigitalOOH.API.Models.Shared.Media;

namespace DigitalOOH.API.Interfaces.Shared.Media
{
    public interface IMediaService
    {
        public Task<MediaUploadResult> UploadAsync(MediaUploadParam param);
        public void DeleteMediaFile(string mediaUrl);
    }
}

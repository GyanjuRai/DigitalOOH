using DigitalOOH.API.Interfaces.Shared.Media;
using DigitalOOH.API.Models.Shared.Enum;
using DigitalOOH.API.Models.Shared.Media;

namespace DigitalOOH.API.Services.Shared.Media
{
    public class MediaService: IMediaService
    {
        private readonly IWebHostEnvironment _env;

        public MediaService(
            IWebHostEnvironment env
            )
        {
            _env = env;
        }

        public async Task<MediaUploadResult> UploadAsync(MediaUploadParam param)
        {
            var extension = Path.GetExtension(param.File.FileName).ToLower();

            ValidateFile(param.MediaType, extension, param.File.Length);

            var folder = param.MediaType == MediaType.Image
                ? "media/images"
                : "media/videos";

            var fileName = $"{Guid.NewGuid()}{extension}";
            var savePath = Path.Combine(_env.WebRootPath, folder, fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(savePath)!);

            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                await param.File.CopyToAsync(stream);
            }

            return new MediaUploadResult
            {
                FileName = fileName,
                Url = $"/{folder}/{fileName}",
                MediaType = param.MediaType
            };
        }

        private static void ValidateFile(MediaType type, string extension, long size)
        {
            var imageExt = new[] { ".jpg", ".jpeg", ".png" };
            var videoExt = new[] { ".mp4", ".webm" };

            if (type == MediaType.Image && !imageExt.Contains(extension))
            {
                throw new NotSupportedException($"Image type {extension} not supported");
            }

            if(type == MediaType.Video && !videoExt.Contains(extension))
            {
                throw new NotSupportedException($"Video type {extension} not supported");
            }

            if(size > 10_000_000)
            {
                throw new NotSupportedException($"File is too large");
            }
        }

        public void DeleteMediaFile(string mediaUrl)
        {
            var filePath = Path.Combine(
                                Directory.GetCurrentDirectory(),
                                "wwwroot",
                                mediaUrl.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString())
                                );

            if(File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        } 
    }
}

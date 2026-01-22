using DigitalOOH.API.Models.Shared.Enum;

namespace DigitalOOH.API.Models.Shared.Media
{
    public record MediaUploadParam
    {
        public MediaType MediaType { get; set; }
        public required IFormFile File { get; set; }
    }
    public class MediaUploadResult
    {
        public string FileName { get; set; } = null!;
        public string Url { get; set; } = null!;
        public MediaType MediaType { get; set; }
    }
}

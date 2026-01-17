using DigitalOOH.API.Models.Shared.Enum;

namespace DigitalOOH.API.Models.Application
{
    public record AdsModel
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required MediaType MediaType { get; set; }
        public string? MediaUrl { get; set; }
        public required int DurationSeconds { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public record AdCreateParam
    {
        public required string Title { get; set; }
        public required MediaType MediaType { get; set; }
        public required int DurationSeconds { get; set; }
        public required IFormFile File { get; set; }
    }

    public record AdsNameAndId
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; }
    }
    public record AdsIdParam
    {
        public required Guid Id { get; set; }
    }
}

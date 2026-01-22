namespace DigitalOOH.API.Models.Application
{
    public record ScreensModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Location { get; set; }
        public required string Resolution { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public record ScreenId
    {
        public required Guid Id { get; set; }
    }
    public record ScreenNameAndId
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
    }
    public record ScreenParam
    {
        public required string Name { get; set; }
        public required string Location { get; set; }
        public required string Resolution { get; set; }
        public required bool IsActive { get; set; }
    }
    public record ScreenEditParam
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Location { get; set; }
        public required string Resolution { get; set; }
        public required bool IsActive { get; set; }
    }

    public record PlayListItem
    {
        public required Guid AdId { get; set; }
        public required string MediaUrl { get; set; }
        public required int DurationSeconds { get; set; }
    }

    public record PlayListItemRequest
    {
        public DateTime At { get; set; }
    }

    public record PlayListQuery
    {
        public DateTime At { get; set; }
    }

    public record PlayListResponse
    {
        public required Guid CampaignId { get; set; }
        public required IEnumerable<PlayListItem> Ads { get; set; }
    }

    public record ProofOfPlayRequest
    {
        public required Guid ScreenId { get; set; }
        public required Guid CampaignId { get; set; }
        public required IEnumerable<PlayListItem> playList { get; set; }
        public required DateTime StartAt { get; set; }
    }
}

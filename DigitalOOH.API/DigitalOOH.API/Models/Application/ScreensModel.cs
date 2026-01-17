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
    public record ScreenIdParam
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
    }
    public record ScreenEditParam
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Location { get; set; }
        public required string Resolution { get; set; }
        public required bool IsActive { get; set; }
    }
}

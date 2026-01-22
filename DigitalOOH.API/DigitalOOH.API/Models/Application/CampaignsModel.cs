namespace DigitalOOH.API.Models.Application
{
    public class CampaignsModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public required string Screens { get; set; }
        public required string Ads { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class CampaignsCreateParam
    {
        public required string Name { get; set; }
        public required DateTime StartTime { get; set; }
        public required DateTime EndTime { get; set; }
        public required List<Guid> Screens { get; set; }
        public required List<AdPlaylistItem> Ads { get; set; }
    }

}

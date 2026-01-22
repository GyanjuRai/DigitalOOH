using DigitalOOH.API.Models.Shared.Enum;

namespace DigitalOOH.API.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
    public class Screen : BaseEntity
    {
        public required string Name { get; set; }
        public required string Location { get; set; }
        public required string Resolution { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<CampaignScreen> CampaignScreens { get; set; } = new List<CampaignScreen>();
    }
    public class  Ad : BaseEntity
    {
        public required string Title { get; set; }
        public MediaType MediaType { get; set; }
        public string? MediaUrl { get; set; } = null;
        public int DurationSeconds { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<CampaignAd> CampaignAds { get; set; } = new List<CampaignAd>();
    }
    public class Campaign : BaseEntity
    {
        public required string Name { get; set; }
        public required DateTime StartTime { get; set; }
        public required DateTime EndTime { get; set; }

        public ICollection<CampaignScreen> CampaignScreens { get; set; } = new List<CampaignScreen>();
        public ICollection<CampaignAd> CampaignAds { get; set; } = new List<CampaignAd>();

    }
    public class CampaignScreen
    {
        public Guid CampaignId { get; set; } // FOR: DB 
        public Campaign Campaign { get; set; } = null!; // FOR: EF Core, Navigating

        public Guid ScreenId { get; set; }
        public Screen Screen { get; set; } = null!;
    }
    public class CampaignAd
    {
        public  Guid CampaignId { get; set; }
        public Campaign Campaign { get; set; } = null!;

        public Guid AdId { get; set; }
        public Ad Ad { get; set; } = null!;

        public int PlayOrder { get; set; }
    }
    public class ProofOfPlay : BaseEntity
    {
        public  Guid ScreenId { get; set; }
        public Screen Screen { get; set; } = null!;

        public Guid AdId { get; set; }
        public Ad Ad { get; set; } = null!;

        public Guid CampaignId { get; set; }
        public Campaign Campaign { get; set; } = null!;

        public DateTime PlayedAt { get; set; }
    }
    public class User : BaseEntity
    {
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
    }
}

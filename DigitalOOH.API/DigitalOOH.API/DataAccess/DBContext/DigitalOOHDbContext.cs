using DigitalOOH.API.Entities;
using DigitalOOH.API.Models.Shared.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalOOH.API.DataAccess.DBContext
{
    public class DigitalOOHDbContext : DbContext
    {
        public DigitalOOHDbContext(DbContextOptions<DigitalOOHDbContext> options)
            : base(options) { }

        public DbSet<Screen> Screens => Set<Screen>();
        public DbSet<Ad> Ads => Set<Ad>();
        public DbSet<Campaign> Campaigns => Set<Campaign>();
        public DbSet<CampaignScreen> CampaignScreens => Set<CampaignScreen>();
        public DbSet<CampaignAd> CampaignAds => Set<CampaignAd>();
        public DbSet<ProofOfPlay> ProofOfPlays => Set<ProofOfPlay>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // DOES: under this assembly, find all classes that : IEntityTypeConfiguration<T> and calls their configure methods
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DigitalOOHDbContext).Assembly);
        }
    }

    #region Screen Configuration
    public class ScreenConfiguration : IEntityTypeConfiguration<Screen>
    {
        public void Configure(EntityTypeBuilder<Screen> builder)
        {
            builder.ToTable("screens");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnType("nvarchar(100)")
                .IsRequired();

            builder.Property(x => x.Resolution)
                .HasColumnType("nvarchar(20)")
                .IsRequired();

            builder.Property(x => x.Location)
                .HasColumnType("nvarchar(200)")
                .IsRequired();

            builder.Property(x => x.IsActive)
                .HasColumnType("bit")
                .HasDefaultValue(true);

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.ToTable("screens",
                            t => t.HasCheckConstraint(
                                "checK_screen_resolution",
                                "Resolution LIKE '[0-9][0-9][0-9]x%' OR Resolution LIKE '[0-9][0-9][0-9][0-9]x%'"
                            )
            );

        }
    }
    #endregion

    #region Ad Configuration
    public class AdConfiguration : IEntityTypeConfiguration<Ad>
    {
        public void Configure(EntityTypeBuilder<Ad> builder)
        {
            builder.ToTable("ads");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasColumnType("nvarchar(100)")
                .IsRequired();

            builder.Property(x => x.MediaType)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.MediaUrl)
                .HasColumnType("nvarchar(300)")
                .IsRequired();

            builder.Property(x => x.DurationSeconds)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.ToTable("ads",
                            t => t.HasCheckConstraint(
                                "check_ad_durationSeconds",
                                "[DurationSeconds] > 0"
                            )
            );
        }
    }
    #endregion

    #region Campaign Configuration
    public class CampaignConfiguration : IEntityTypeConfiguration<Campaign>
    {
        public void Configure(EntityTypeBuilder<Campaign> builder)
        {
            builder.ToTable("campaigns");
            
            builder.HasKey(x => x.Id)
                .HasName("campaign_id");

            builder.Property(x => x.Name)
                .HasColumnType("nvarchar(100)")
                .IsRequired();
        
            builder.Property(x => x.StartTime)
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(x => x.EndTime)
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.ToTable("campaigns",
                            t => t.HasCheckConstraint(
                                "check_campaign_timeframe",
                                "[EndTime] > [StartTime]"
                            )
            );
        }

    }
    #endregion

    #region CampaignScreen and CampaignAd Configuration
    public class CampaignScreenConfiguration : IEntityTypeConfiguration<CampaignScreen>
    {
        public void Configure(EntityTypeBuilder<CampaignScreen> builder)
        {
            builder.ToTable("campaign_screens");

            builder.HasKey(x => new { x.CampaignId, x.ScreenId });

            builder.HasOne(x => x.Campaign)
                .WithMany(x => x.CampaignScreens)
                .HasForeignKey(x => x.CampaignId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Screen)
                .WithMany(x => x.CampaignScreens)
                .HasForeignKey(x => x.ScreenId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class CampaignAdConfiguration : IEntityTypeConfiguration<CampaignAd>
    {
        public void Configure(EntityTypeBuilder<CampaignAd> builder)
        {
            builder.ToTable("campaign_ads");

            builder.HasKey(x => new { x.CampaignId, x.AdId });

            builder.Property(x => x.PlayOrder)
                .HasColumnType("int")
                .IsRequired();
            
            builder.HasOne(x => x.Campaign)
                .WithMany(x => x.CampaignAds)
                .HasForeignKey(x => x.CampaignId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(x => x.Ad)
                .WithMany(x => x.CampaignAds)
                .HasForeignKey(x => x.AdId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
    #endregion

    #region ProofOfPlay Configuration
    public class ProofOfPlayConfiguration : IEntityTypeConfiguration<ProofOfPlay>
    {
        public void Configure(EntityTypeBuilder<ProofOfPlay> builder)
        {
            builder.ToTable("proof_of_plays");

            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.PlayedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();
            
            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(x => x.Campaign)
                .WithMany()
                .HasForeignKey(x => x.CampaignId);

            builder.HasOne(x => x.Ad)
                .WithMany()
                .HasForeignKey(x => x.AdId);

            builder.HasOne(x => x.Screen)
                .WithMany()
                .HasForeignKey(x => x.ScreenId);
        }
    }
    #endregion

    #region User Configuration
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Email)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.PasswordHash)
                .HasColumnType("nvarchar(200)")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
    #endregion
}

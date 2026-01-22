using System.ComponentModel.DataAnnotations;
using DigitalOOH.API.DataAccess.DBContext;
using DigitalOOH.API.Entities;
using DigitalOOH.API.Interfaces.Application.Campaigns;
using DigitalOOH.API.Models.Application;
using DigitalOOH.API.Models.Shared.Response;
using Microsoft.EntityFrameworkCore;

namespace DigitalOOH.API.Services.Application.Campaigns
{
    public class CampaignsService : ICampaignsService
    {
        private readonly DigitalOOHDbContext _context;
        public CampaignsService(
            DigitalOOHDbContext context
            ) 
        {
            _context = context;
        }

        public async Task<GridResponse<CampaignsModel>> GetCampaigns()
        {
            var campaigns = await _context.Campaigns
                            .Select(c => new CampaignsModel
                            {
                                Id = c.Id,
                                Name = c.Name,
                                StartTime = c.StartTime,
                                EndTime = c.EndTime,
                                Screens = string.Join(", ", 
                                                    c.CampaignScreens.Select(c  => c.Screen.Name)),
                                Ads = string.Join(", ",
                                                c.CampaignAds.Select(a => a.Ad.Title)),
                                CreatedAt = c.CreatedAt
                            })
                            .ToListAsync();

            return new GridResponse<CampaignsModel>
            {
                Data = campaigns,
                TotalRows = campaigns.Count()
            };
        }

        public async Task<CampaignsModel> AddCampaign(CampaignsCreateParam param)
        {
            
            await CheckOverLap(param);

            var campaignId = Guid.NewGuid();

            var campaigns = new Campaign
            {
                Id = campaignId,
                Name = param.Name,
                StartTime = param.StartTime,
                EndTime = param.EndTime,
                CampaignScreens = param.Screens
                                    .Select(id => new CampaignScreen
                                    {
                                        CampaignId = campaignId,
                                        ScreenId = id
                                    })
                                    .ToList(),
                CampaignAds = param.Ads
                                .Select(a => new CampaignAd
                                {
                                    CampaignId = campaignId,
                                    AdId = a.Id,
                                    PlayOrder = a.PlayOrder
                                })
                                .ToList(),
                CreatedAt = DateTime.UtcNow
            };

           _context.Campaigns.Add(campaigns);
            await _context.SaveChangesAsync();

            return new CampaignsModel
            {
                Id = campaigns.Id,
                Name = campaigns.Name,
                StartTime = campaigns.StartTime,
                EndTime = campaigns.EndTime,
                Screens = string.Join(", ", campaigns.CampaignScreens.Select(c => c.Screen.Name)),
                Ads = string.Join(", ", campaigns.CampaignAds.Select(a => a.Ad.Title)),
                CreatedAt = campaigns.CreatedAt
            };
        }

        private async Task CheckOverLap(CampaignsCreateParam newCampaign)
        {
            var screenIds = newCampaign.Screens;

            var overlap = await _context.Campaigns
                    .AnyAsync( c =>
                        c.CampaignScreens.Any(cs => screenIds.Contains(cs.ScreenId)) &&
                        (
                            c.StartTime < newCampaign.EndTime &&
                            newCampaign.StartTime < c.EndTime

                        )
                );
            
            if(overlap)
            {
                throw new ValidationException("Campaign time overlaps with existing campaigns on the same screens.");
            }
        }

    }
}

using DigitalOOH.API.DataAccess.DBContext;
using DigitalOOH.API.Entities;
using DigitalOOH.API.Interfaces.Application.Ads;
using DigitalOOH.API.Interfaces.Shared.Media;
using DigitalOOH.API.Models.Application;
using DigitalOOH.API.Models.Shared.Media;
using DigitalOOH.API.Models.Shared.Response;
using Microsoft.EntityFrameworkCore;

namespace DigitalOOH.API.Services.Application.Ads
{
    public class AdsService : IAdsService
    {
        private readonly DigitalOOHDbContext _context;
        private readonly IMediaService _mediaService;
        public AdsService(
            DigitalOOHDbContext context,
            IMediaService mediaService
            ) 
        { 
            _context = context;
            _mediaService = mediaService;
        }

        public async Task<GridResponse<AdsModel>> GetAds()
        {
            var ads = await _context.Ads
                .AsNoTracking()
                .Select(a => new AdsModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    MediaType = a.MediaType,
                    MediaUrl = a.MediaUrl,
                    DurationSeconds = a.DurationSeconds,
                    CreatedAt = a.CreatedAt
                })
                .ToListAsync();

            return new GridResponse<AdsModel>{
                Data = ads,
                TotalRows = ads.Count
            };
        }

        public async Task<List<AdsNameAndId>> GetAdsNamesAndId()
        {
            var ads = await _context.Ads
                .AsNoTracking()
                .Select(a => new AdsNameAndId
                {
                    Id = a.Id,
                    Title = a.Title
                })
                .ToListAsync();

            return ads;
        }

        public async Task<AdsModel> AdsAdd(AdCreateParam param)
        {
            MediaUploadResult? mediaResult = null;
            try
            {
                var media = new MediaUploadParam
                {
                    File = param.File,
                    MediaType = param.MediaType
                };

                mediaResult = await _mediaService.UploadAsync(media);

                var ad = new Ad
                {
                    Id = Guid.NewGuid(),
                    Title = param.Title,
                    MediaType = param.MediaType,
                    MediaUrl = mediaResult.Url,
                    DurationSeconds = param.DurationSeconds,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Ads.Add(ad);
                await _context.SaveChangesAsync();


                return new AdsModel
                {
                    Id = ad.Id,
                    Title = ad.Title,
                    MediaType = ad.MediaType,
                    MediaUrl = ad.MediaUrl,
                    DurationSeconds = ad.DurationSeconds,
                    CreatedAt = DateTime.UtcNow
                };
            }
            catch
            {
                if(mediaResult != null)
                {
                    _mediaService.DeleteMediaFile(mediaResult.Url);
                }
                throw;
            }
        }

        public async Task<bool> AdsRemove(AdsIdParam param)
        {
            if (await CheckAdInUse(param))
            {
                throw  new InvalidOperationException("Ad is currently in use and cannot be deleted.");
            }
         

            var ad = await _context.Ads
                .FirstOrDefaultAsync(a => a.Id == param.Id);

            if (ad == null)
            {
                throw new KeyNotFoundException("Ad not found.");
            } 

            if (!string.IsNullOrWhiteSpace(ad.MediaUrl))
            {
                _mediaService.DeleteMediaFile(ad.MediaUrl);
            }

            _context.Ads.Remove(ad);
            await _context.SaveChangesAsync();

            return true;
        }

        private async Task<bool> CheckAdInUse(AdsIdParam param)
        {
            var now = DateTime.UtcNow;

            var isUsed =  await _context.CampaignAds
            .AnyAsync(ca => 
            ca.AdId == param.Id
            );

            return isUsed;
        }
    }
}

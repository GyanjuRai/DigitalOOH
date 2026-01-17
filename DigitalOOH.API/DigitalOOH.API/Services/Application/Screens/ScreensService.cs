using DigitalOOH.API.DataAccess.DBContext;
using DigitalOOH.API.Entities;
using DigitalOOH.API.Interfaces.Application.Screens;
using DigitalOOH.API.Models.Application;
using DigitalOOH.API.Models.Shared.Response;
using Microsoft.EntityFrameworkCore;

namespace DigitalOOH.API.Services.Application.Screens
{
    public class ScreensService : IScreeensService
    {
        private readonly DigitalOOHDbContext _context;
        
        public ScreensService(
            DigitalOOHDbContext context            
            ) 
        {
            _context = context;
        }

        public async Task<GridResponse<ScreensModel>> GetScreens()
        {
            var screens = await _context.Screens
                .AsNoTracking()
                .Select(s => new ScreensModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Location = s.Location,
                    Resolution = s.Resolution,
                    IsActive = s.IsActive,
                    CreatedAt = s.CreatedAt,
                    UpdatedAt = s.UpdatedAt
                })
                .ToListAsync();

            return new GridResponse<ScreensModel> 
            { 
                Data = screens,
                TotalRows = screens.Count
            };
        } 

        public async Task<List<ScreenNameAndId>> GetScreenNameAndId()
        {
            var screens = await _context.Screens
                .AsNoTracking()
                .Select(s => new ScreenNameAndId 
                { 
                    Name = s.Name,
                    Id = s.Id,
                })
                .ToListAsync();

            return screens;
        }

        public async Task<ScreensModel> ScreenAdd(ScreenParam param)
        {
            var screen = new Screen
            {
                Id = Guid.NewGuid(),
                Name = param.Name,
                Location = param.Location,
                Resolution = param.Resolution,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
            
            _context.Screens.Add(screen);
            await _context.SaveChangesAsync();

            return new ScreensModel
            {
                Id = screen.Id,
                Name = screen.Name,
                Location = screen.Location,
                Resolution = screen.Resolution,
                IsActive = screen.IsActive,
                CreatedAt = screen.CreatedAt
            };
        }

        public async Task<ScreensModel?> ScreenEdit(ScreenEditParam param)
        {
            var screen = await _context.Screens
                .FirstOrDefaultAsync(s => s.Id == param.Id);

            if (screen == null) return null;

            screen.Name = param.Name;
            screen.Location = param.Location;
            screen.Resolution = param.Resolution;
            screen.IsActive = param.IsActive;
            screen.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new ScreensModel
            {
                Id = screen.Id,
                Name = screen.Name,
                Location = screen.Location,
                Resolution = screen.Resolution,
                IsActive = param.IsActive,
                UpdatedAt = screen.UpdatedAt
            };
        }
    }
}

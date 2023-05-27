using Microsoft.EntityFrameworkCore;
using NightSky.API.Constants;
using NightSky.API.DAL;
using NightSky.API.Exceptions;

namespace NightSky.API.Services;

public interface IStarsToConstellationsService
{
    public Task AddStarToConstellation(int starId, int constellationId);
    public Task RemoveStarFromConstellation(int starId, int constellationId);
}

public class StarsToConstellationsService : IStarsToConstellationsService
{
    private readonly ApplicationDbContext _context;

    public StarsToConstellationsService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddStarToConstellation(int starId, int constellationId)
    {
        var star = await _context.Stars.Include(e => e.Constallations).FirstOrDefaultAsync(e => e.StarId == starId);
        if (star is null)
        {
            throw new BaseNightSkyException(x =>
            {
                x.AddError(Langs.English, "Star does not exists")
                    .AddError(Langs.Polish, "Gwiazda o podanym id nie istnieje");
            });
        }

        var constellation = await _context.Constellations.FirstOrDefaultAsync(e => e.ConstallationId == constellationId);
        if (constellation is null)
        {
            throw new BaseNightSkyException(x =>
            {
                x.AddError(Langs.English, "Constellation does not exists")
                    .AddError(Langs.Polish, "Konstalacja o podanym id nie istnieje");
            });
        }

        star.Constallations.Add(constellation);
        await _context.SaveChangesAsync();
    }
    public async Task RemoveStarFromConstellation(int starId, int constellationId)
    {
        var star = await _context.Stars.FirstOrDefaultAsync(e => e.StarId == starId);
        if (star is null)
        {
            throw new BaseNightSkyException(x =>
            {
                x.AddError(Langs.English, "Star does not exists")
                    .AddError(Langs.Polish, "Gwiazda o podanym id nie istnieje");
            });
        }

        var constellation = await _context.Constellations.FirstOrDefaultAsync(e => e.ConstallationId == constellationId);
        if (constellation is null)
        {
            throw new BaseNightSkyException(x =>
            {
                x.AddError(Langs.English, "Constellation does not exists")
                    .AddError(Langs.Polish, "Konstalacja o podanym id nie istnieje");
            });
        }

        star.Constallations.Remove(constellation);
        await _context.SaveChangesAsync();
    }

}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NightSky.API.Constants;
using NightSky.API.DAL;
using NightSky.API.DAL.Entities;
using NightSky.API.Exceptions;
using NightSky.API.Models;

namespace NightSky.API.Services;

public interface IStarService
{
    public Task<int> AddStar(StarModel model);
    public Task UpdateStar(StarModel model);
    public Task DeleteStar(int id);
    public Task<StarModel> GetById(int id);
    public Task<ICollection<StarModel>> GetAll();

}

public class StarService : IStarService
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;

    public StarService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> AddStar(StarModel model)
    {
        if (model.StarId is not null)
        {
            throw new BaseNightSkyException(x =>
            {
                x.AddError(Langs.English, "Star already exists")
                    .AddError(Langs.Polish, "Gwiazda o tym id już istnieje");
            });
        }
        
        var entity = _mapper.Map<Star>(model);
        _context.Add(entity);
        await _context.SaveChangesAsync();

        return entity.StarId;
    }

    public async Task UpdateStar(StarModel model)
    {
        var existingEntity = await GetStarById(model.StarId);
        
        var _ = _mapper.Map(model, existingEntity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteStar(int id)
    {
        var entity = await GetStarById(id);

        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<StarModel> GetById(int id)
    {
        var entity = await GetStarById(id);
        return _mapper.Map<StarModel>(entity);
    }

    public async Task<ICollection<StarModel>> GetAll()
    {
        var starsEntities = await _context.Stars.ToListAsync();
        var stars = starsEntities.Select(e => _mapper.Map<StarModel>(e));

        return stars.ToList();
    }

    private async Task<Star> GetStarById(int? id)
    {
        if (id is null)
        {
            throw new BaseNightSkyException(x =>
            {
                x.AddError(Langs.English, "Invalid id provided")
                    .AddError(Langs.Polish, "Podano nieprawidłowy ID");
            });
        }
        var entity = await _context.Stars.FirstOrDefaultAsync(e => e.StarId == id);
        if (entity is null)
        {
            throw new StarDoesNotExistException();
        }

        return entity;
    }
}
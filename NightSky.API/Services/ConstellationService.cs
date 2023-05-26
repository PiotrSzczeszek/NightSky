using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NightSky.API.Constants;
using NightSky.API.DAL;
using NightSky.API.DAL.Entities;
using NightSky.API.Exceptions;
using NightSky.API.Models;

namespace NightSky.API.Services;

public interface IConstellationService
{
    public Task<int> AddConstellation(ConstellationModel model);
    public Task UpdateConstellation(ConstellationModel model);
    public Task DeleteConstellation(int id);
    public Task<ConstellationModel> GetById(int id);
    public Task<ICollection<ConstellationModel>> GetAll();

}

public class ConstellationService : IConstellationService
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;

    public ConstellationService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> AddConstellation(ConstellationModel model)
    {
        if (model.ConstallationId is not null)
        {
            throw new BaseNightSkyException(x =>
            {
                x.AddError(Langs.English, "Constellation already exists")
                    .AddError(Langs.Polish, "Gwiazda o tym id już istnieje");
            });
        }
        
        var entity = _mapper.Map<Constellation>(model);
        _context.Add(entity);
        await _context.SaveChangesAsync();

        return entity.ConstallationId;
    }

    public async Task UpdateConstellation(ConstellationModel model)
    {
        var existingEntity = await GetConstellationById(model.ConstallationId);
        
        var _ = _mapper.Map(model, existingEntity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteConstellation(int id)
    {
        var entity = await GetConstellationById(id);

        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<ConstellationModel> GetById(int id)
    {
        var entity = await GetConstellationById(id);
        return _mapper.Map<ConstellationModel>(entity);
    }

    public async Task<ICollection<ConstellationModel>> GetAll()
    {
        var starsEntities = await _context.Constellations.ToListAsync();
        var stars = starsEntities.Select(e => _mapper.Map<ConstellationModel>(e));

        return stars.ToList();
    }

    private async Task<Constellation> GetConstellationById(int? id)
    {
        if (id is null)
        {
            throw new BaseNightSkyException(x =>
            {
                x.AddError(Langs.English, "Invalid id provided")
                    .AddError(Langs.Polish, "Podano nieprawidłowy ID");
            });
        }
        var entity = await _context.Constellations.FirstOrDefaultAsync(e => e.ConstallationId == id);
        if (entity is null)
        {
            throw new ConstellationDoesNotExistException();
        }

        return entity;
    }
}
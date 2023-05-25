using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NightSky.API.Constants;
using NightSky.API.DAL;
using NightSky.API.DAL.Entities;
using NightSky.API.Exceptions;
using NightSky.API.Models;

namespace NightSky.API.Services;

public interface ISkyDataService
{
    public Task<int> AddSkyData(SkyDataModel model);
    public Task UpdateSkyData(SkyDataModel model);
    public Task<SkyDataModel> GetById(int id);
    public Task<ICollection<SkyDataModel>> GetDatesRange(DateTime startDate, DateTime endDate);
}

public class SkyDataService : ISkyDataService
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;

    public SkyDataService(IMapper mapper, ApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<int> AddSkyData(SkyDataModel model)
    {
        if (model.SkyId is not null)
        {
            throw new BaseNightSkyException(x =>
            {
                x.AddError(Langs.English, "This id already exists")
                    .AddError(Langs.Polish, "Ten identyfikator już istnieje");
            });
        }

        var dataForDay = await _context.SkyData.FirstOrDefaultAsync(e => e.DataDate == model.DataDate);
        if (dataForDay is not null)
        {
            throw new BaseNightSkyException(x =>
            {
                x.AddError(Langs.English, "Sky data for this date already exists")
                    .AddError(Langs.Polish, "Dane nieba dla tego dnia już istnieją");
            });
        }

        var entity = _mapper.Map<SkyData>(model);
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity.SkyId;
    }

    public async Task UpdateSkyData(SkyDataModel model)
    {
        var existingEntity = await GetSkyDataById(model.SkyId);
        var _ = _mapper.Map(model, existingEntity);

        await _context.SaveChangesAsync();
    }

    public async Task<SkyDataModel> GetById(int id)
    {
        var existingEntity = await GetSkyDataById(id);
        return _mapper.Map<SkyDataModel>(existingEntity);
    }

    public async Task<ICollection<SkyDataModel>> GetDatesRange(DateTime startDate, DateTime endDate)
    {
        var entities = await _context.SkyData.Where(e => e.DataDate >= startDate.Date && e.DataDate <= endDate.Date).ToListAsync();
        
        return entities.Select(e => _mapper.Map<SkyDataModel>(e)).ToList();
    }

    private async Task<SkyData> GetSkyDataById(int? id)
    {
        if (id is null)
        {
            throw new BaseNightSkyException(x =>
            {
                x.AddError(Langs.English, "Invalid id provided")
                    .AddError(Langs.Polish, "Podano nieprawidłowy ID");
            });
        }

        var entity = await _context.SkyData.FirstOrDefaultAsync(e => e.SkyId == id);
        if (entity is null)
        {
            throw new SkyDataDoesNotExistException();
        }

        return entity;
    }
}
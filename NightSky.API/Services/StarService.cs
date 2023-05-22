using AutoMapper;
using NightSky.API.DAL;
using NightSky.API.Models;

namespace NightSky.API.Services;

public class StarService
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;

    public StarService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task AddStar(StarModel model)
    {
        
    }
}
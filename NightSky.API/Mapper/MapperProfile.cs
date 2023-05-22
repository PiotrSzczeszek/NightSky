using AutoMapper;
using NightSky.API.DAL.Entities;
using NightSky.API.Models;

namespace NightSky.API.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Star, StarModel>().ReverseMap();
        CreateMap<Constellation, ConstellationModel>().ReverseMap();
    }
}
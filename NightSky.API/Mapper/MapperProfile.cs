using AutoMapper;
using NightSky.API.DAL.Entities;
using NightSky.API.Models;

namespace NightSky.API.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Star, StarModel>().ReverseMap();
        CreateMap<SkyData, SkyDataModel>().ReverseMap();


        CreateMap<Constellation, ConstellationModel>();
        CreateMap<ConstellationModel, Constellation>()
            .ForMember(e => e.Stars, opt => opt.Ignore());
        // .AfterMap(AddOrUpdateStars);

    }
    
}
using System.Reflection;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Microsoft.EntityFrameworkCore;
using NightSky.API.DAL;
using NightSky.API.Services;

namespace NightSky.API.Extensions;

public static class ServicesExtension
{
    public static IServiceCollection RegisterDbContext(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite("Data Source=./DAL/application_database.db");
        });

        using var provider = serviceCollection.BuildServiceProvider();
        var context = provider.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureCreated();

        return serviceCollection;
    }

    public static IServiceCollection RegisterAutoMapper(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(Assembly.GetCallingAssembly());
        
        return serviceCollection;
    }

    public static IServiceCollection RegisterServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IStarService, StarService>();
        serviceCollection.AddTransient<IStarsToConstellationsService, StarsToConstellationsService>();
        serviceCollection.AddTransient<ISkyDataService, SkyDataService>();
        serviceCollection.AddTransient<IConstellationService, ConstellationService>();
        
        return serviceCollection;
    }
}
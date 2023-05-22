using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NightSky.API.DAL;

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
}
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using NightSky.API.DAL;

namespace NightSky.Migrations;

public class ApplicationDbContextDesignTimeFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder()
            .UseSqlite("Data Source=../NightSky.API/DAL/application_database.db",
                x => x.MigrationsAssembly("NightSky.Migrations"));
            
        var context = new ApplicationDbContext(options.Options);

        return context;
    }
}
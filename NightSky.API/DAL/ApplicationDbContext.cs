using Microsoft.EntityFrameworkCore;
using NightSky.API.DAL.Entities;

namespace NightSky.API.DAL;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    { }

    public ApplicationDbContext()
    {
        
    }

    public virtual DbSet<Star> Stars { get; set; }
    public virtual DbSet<Constellation> Constellations { get; set; }
    public virtual DbSet<SkyData> SkyData { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Constellation>(entity =>
        {
            entity.ToTable("Constellation");

            entity.Property(e => e.ConstallationId)
                .ValueGeneratedOnAdd();
            
            entity.HasKey(e => e.ConstallationId);
            entity.HasMany<Star>(e => e.Stars)
                .WithMany(e => e.Constallations);
        });

        modelBuilder.Entity<Star>(entity =>
        {
            entity.Property(e => e.StarId).ValueGeneratedOnAdd();
            entity.HasKey(e => e.StarId);
        });

        modelBuilder.Entity<SkyData>(entity =>
        {
            entity.Property(e => e.SkyId).ValueGeneratedOnAdd();
            entity.HasKey(e => e.SkyId);
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}
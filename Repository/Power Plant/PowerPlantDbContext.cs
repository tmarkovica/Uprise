using Microsoft.EntityFrameworkCore;
using Uprise.Repository.Power_Plant.Models;

namespace Uprise.Repository.Power_Plant;

public class PowerPlantDbContext : DbContext
{
    public const string SCHEMA = "plants";

    public PowerPlantDbContext(DbContextOptions<PowerPlantDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(SCHEMA);
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PowerPlant>();
        modelBuilder.Entity<PowerProduction>();
    }

    public DbSet<PowerPlant> PowerPlants { get; set; }

    public DbSet<PowerProduction> RealProductions { get; set; }
}

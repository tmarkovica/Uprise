using Microsoft.EntityFrameworkCore;
using Npgsql;
using Uprise.Repository.Power_Plant;
using Uprise.Repository.Power_Plant.Models;

namespace Uprise.Services;

public class PowerPlantService
{
    private readonly PowerPlantDbContext _db;
    private readonly ILogger<PowerPlantService> _logger;

    public PowerPlantService(PowerPlantDbContext db, ILogger<PowerPlantService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<PowerPlant> Create(PowerPlant request)
    {
        var res = await _db.PowerPlants.FromSqlRaw(
            $"INSERT INTO {PowerPlant.Table()} (name, installed_power, latitude, longitude, date_of_installation) " +
            "VALUES (@p1, @p2, @p3, @p4, @p5) RETURNING *",
            new NpgsqlParameter[]
            {
                new NpgsqlParameter("@p1", request.Name),
                new NpgsqlParameter("@p2", request.InstalledPower),
                new NpgsqlParameter("@p3", request.Latitude),
                new NpgsqlParameter("@p4", request.Longitude),
                new NpgsqlParameter("@p5", request.DateOfInstallation)
            }
            )
            .ToListAsync();
        return res.First();
    }

    public async Task<List<PowerPlant>> Read()
    {
        return await _db.PowerPlants.ToListAsync();
    }

    public async Task<PowerPlant> Read(int id)
    {
        return await _db.PowerPlants.SingleAsync(p => p.Id == id);
    }

    public async Task<PowerPlant> Update(PowerPlant request)
    {
        PowerPlant entity = await _db.PowerPlants.SingleAsync(p => p.Id == request.Id);
        entity.Name = request.Name;
        entity.InstalledPower = request.InstalledPower;
        entity.Latitude = request.Latitude;
        entity.Longitude = request.Longitude;
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<PowerPlant> UpdateName(PowerPlant request)
    {
        PowerPlant entity = await _db.PowerPlants.SingleAsync(p => p.Id == request.Id);
        entity.Name = request.Name;
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task Delete(int id)
    {
        await _db.Database.ExecuteSqlRawAsync(
            $"DELETE FROM {PowerPlantDbContext.SCHEMA}.{PowerPlant.TABLENAME} WHERE id = {id}"
            );
    }
}

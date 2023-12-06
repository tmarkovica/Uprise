using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Text;
using Uprise.Repository.Power_Plant;
using Uprise.Repository.Power_Plant.Models;

namespace Uprise.Services;

public class PowerPlantProductionService
{
    private readonly PowerPlantDbContext _db;
    private readonly WeatherForecastService _weatherService;
    private readonly ILogger<PowerPlantDbContext> _logger;

    public PowerPlantProductionService(
        PowerPlantDbContext db, WeatherForecastService weatherService, ILogger<PowerPlantDbContext> logger)
    {
        _db = db;
        _weatherService = weatherService;
        _logger = logger;
    }

    public async Task<List<RealProduction>> Get_RealProduction(int powerPlantId, int granularity = 15, int timespan = 60)
    {
        var res = await _db.RealProductions.Where(
            p => (p.PowerPlantId == powerPlantId && p.Timestamp > DateTime.Now.AddMinutes(-1 * timespan))
            ).ToListAsync();
        res.OrderByDescending(p => p.Timestamp);

        if (res.Count > 0)
        {
            DateTime timestampOfLastIncludedRecord = res[0].Timestamp;
            DateTime timespanBoundary = res[0].Timestamp.AddMinutes(-1 * granularity);
            for (int i = 0; i < res.Count; i++)
            {
                if (res[i].Timestamp > timespanBoundary && res[i].Timestamp < timestampOfLastIncludedRecord)
                {
                    res.RemoveAt(i);
                    i--;
                }
                else
                {
                    timestampOfLastIncludedRecord = res[i].Timestamp;
                    timespanBoundary = res[i].Timestamp.AddMinutes(-1 * granularity);
                }
            }
        }
        return res;
    }

    private double PredictPowerProduction(string installedPower, string forecast)
    {
        return new Random().NextDouble() / 10000;
    }

    private List<RealProduction> GetProductionPredictionsBasedOnWeatherForecast(
        List<DateTime> timestamps, string installedPower, string forecast)
    {
        List<RealProduction> forecastedProductions = new();
        for (int i = 0; i < timestamps.Count; i++)
        {
            forecastedProductions.Add(new RealProduction()
            {
                Power = PredictPowerProduction(installedPower, forecast),
                Timestamp = timestamps[i]
            });
        }
        return forecastedProductions;
    }

    public async Task<List<RealProduction>> Get_ForecastedProduction(int powerPlantId, int granularity = 15, int timespan = 60)
    {
        PowerPlant powerPlant = await _db.PowerPlants.SingleAsync(p => p.Id == powerPlantId);
        return GetProductionPredictionsBasedOnWeatherForecast(
            GetTimestamps(timespan, granularity), 
            powerPlant.InstalledPower,
            await _weatherService.GetWeatherForecastSimple(powerPlant.Latitude, powerPlant.Longitude, 2)
            );
    }

    public async Task Create_RealProductionRecordsForPowerPlant(List<RealProduction> records, int powerPlantId)
    {
        StringBuilder sb = new();
        var parameters = new List<NpgsqlParameter>()
        {
            new NpgsqlParameter($"@pPowerPlantId", powerPlantId)
        };
        for (int i = 0; i < records.Count; i++)
        {
            parameters.Add(new NpgsqlParameter($"@pPower{i}", records[i].Power));
            parameters.Add(new NpgsqlParameter($"@pTimestamp{i}", records[i].Timestamp));

            sb.Append($"(@pPowerPlantId, @pPower{i}, @pTimestamp{i})");
            if (i < records.Count - 1)
                sb.Append(", ");
        }
        _ = await _db.Database.ExecuteSqlRawAsync(
            $"INSERT INTO {RealProduction.Table()} (power_plant_id, power, timestamp) VALUES {sb}",
            parameters.ToArray()
            );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="timespan">Timespan in minutes.</param>
    /// <param name="granularity">Positive granularity gets timestamps for the future, negative
    /// granularity gets timestamps for past.</param>
    /// <returns></returns>
    private List<DateTime> GetTimestamps(int timespan, int granularity)
    {
        var timestamps = new List<DateTime>();
        DateTime time = DateTime.Now;
        timestamps.Add(time);
        for (int i = 1; i < timespan / Math.Abs(granularity); i++)
        {
            time = time.AddMinutes(granularity);
            timestamps.Add(time);
        }
        return timestamps;
    }

    /// <summary>
    /// Create Seed data function that will generate historical production data for power plants in the 
    /// database allowing API testing -> solar power plant data and associated timeseries can be 
    /// randomly generated.
    /// </summary>
    /// <returns></returns>
    public async Task SeedData()
    {
        var allPowerPlants = await _db.PowerPlants.ToListAsync();
        Random rng = new();
        List<DateTime> timestamps = GetTimestamps(24 * 60, -15);

        foreach (var powerPlant in allPowerPlants)
        {
            List<RealProduction> realProductions = new();
            for (int i = 0; i < timestamps.Count; i++)
            {
                realProductions.Add(new RealProduction()
                {
                    Power = rng.Next() / 10000,
                    Timestamp = timestamps[i]
                });
            }
            await Create_RealProductionRecordsForPowerPlant(realProductions, powerPlant.Id);
        }
    }
}

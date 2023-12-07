using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Uprise.Dto;
using Uprise.Services;

namespace Uprise.Controllers;

// https://rapidapi.com/weatherapi/api/weatherapi-com

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly WeatherForecastService _forecastService;
    private readonly PowerPlantService _powerPlantService;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(
        WeatherForecastService service, PowerPlantService powerPlantService, ILogger<WeatherForecastController> logger)
    {
        _forecastService = service;
        _powerPlantService = powerPlantService;
        _logger = logger;
    }

    [HttpGet("")]
    public async Task<ActionResult<WeatherForecastDto>> GetWehatherForecast(
        [FromQuery] float lat, [FromQuery] float lon, [FromQuery] int days)
    {
        try
        {
            return Ok(await _forecastService.GetWeatherForecast(lat, lon, days));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{powerPlantId}")]
    public async Task<ActionResult<string>> GetWeatherForecastAroundPowerPlant(int powerPlantId)
    {
        try
        {
            var powerPlant = await _powerPlantService.Read(powerPlantId);
            return Ok(await _forecastService.GetWeatherForecastSimple(powerPlant.Latitude, powerPlant.Longitude, 2));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
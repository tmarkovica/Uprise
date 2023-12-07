using Microsoft.AspNetCore.Mvc;
using Uprise.Authentication;
using Uprise.Requests;
using Uprise.Repository.Power_Plant.Models;
using Uprise.Services;
using Uprise.Dto;
using Uprise.Constants;

namespace Uprise.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PowerPlantController : ControllerBase
{
    private readonly PowerPlantService _service;
    private readonly PowerPlantProductionService _productionService;
    private readonly ILogger<PowerPlantController> _logger;

    public PowerPlantController(
        PowerPlantService service, 
        PowerPlantProductionService productionService, 
        ILogger<PowerPlantController> logger)
    {
        _service = service;
        _productionService = productionService;
        _logger = logger;
    }

    [UserAuthFilter]
    [HttpPost("")]
    public async Task<ActionResult<PowerPlantProductionRequest>> CreatePowerPlant([FromBody] PowerPlant request)
    {
        try
        {
            return Ok(await _service.Create(request));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [UserAuthFilter]
    [HttpPut("")]
    public async Task<ActionResult<PowerPlantProductionRequest>> UpdatePowerPlant([FromBody] PowerPlant request)
    {
        try
        {
            return Ok(await _service.Update(request));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [UserAuthFilter]
    [HttpPatch("")]
    public async Task<ActionResult<PowerPlantProductionRequest>> UpdatePowerPlantName([FromBody] PowerPlant request)
    {
        try
        {
            return Ok(await _service.UpdateName(request));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [UserAuthFilter]
    [HttpDelete("{id}")]
    public async Task<ActionResult<PowerPlantProductionRequest>> DeletePowerPlant(int id)
    {
        try
        {
            await _service.Delete(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [UserAuthFilter]
    [HttpGet("")]
    public async Task<ActionResult<List<PowerPlantProductionRequest>>> GetAllPowerPlants()
    {
        try
        {
            return Ok(await _service.Read());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [UserAuthFilter]
    [HttpGet("{id}")]
    public async Task<ActionResult<PowerPlantProductionRequest>> GetPowerPlant(int id)
    {
        try
        {
            return Ok(await _service.Read(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [UserAuthFilter]
    [HttpGet("{id}/production")]
    public async Task<ActionResult<PowerProduction>> GetPowerPlantProduction(
        int id, [FromQuery] PowerPlantProductionRequest query)
    {
        try
        {
            if (query.TimeseriesType.ToLower() == ProductionType.REAL)
                return Ok(await _productionService.Get_RealProduction(id, query.TimeseriesGranularity, query.TimeseriesTimespan));
            else if (query.TimeseriesType.ToLower() == ProductionType.FORECASTED)
                return Ok(await _productionService.Get_ForecastedProduction(id, query.TimeseriesGranularity, query.TimeseriesTimespan));
            else
                throw new Exception("TimeseriesType is not specified correctly.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [UserAuthFilter]
    [HttpGet("SeedData")]
    public async Task<ActionResult<List<PowerPlantProductionRequest>>> GenerateDummyDataForRealProductionDataOfPowerPlants()
    {
        try
        {
            await _productionService.SeedData();
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Uprise.Requests;

public class PowerPlantProductionRequest
{
    [JsonPropertyName("timeseries_type")]
    public string TimeseriesType { get; set; } = null!; // real production or forecasted production

    [Required]
    [JsonPropertyName("timeseries_granularity")]
    public int TimeseriesGranularity { get; set; } // 15 minutes or 1 hour

    [Required]
    [JsonPropertyName("timeseries_timespan")]
    public int TimeseriesTimespan { get; set; } // minutes
}
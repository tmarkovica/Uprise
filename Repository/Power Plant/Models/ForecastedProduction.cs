using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Uprise.Repository.Power_Plant.Models;

[Table("forecasted_productions", Schema = PowerPlantDbContext.SCHEMA)]
[Keyless]
public partial class ForecastedProduction
{
    [JsonIgnore]
    [Column("power_plant_id", TypeName = "integer")]
    [ForeignKey("FK_power_plants_forecasted_productions")]
    public int PowerPlantId { get; set; }

    [Column("power")]
    public double Power { get; set; } // GWh

    [Column("timestamp")]
    public DateTime Timestamp { get; set; }

    [JsonIgnore]
    public PowerPlant PowerPlant { get; set; }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Uprise.Repository.Power_Plant.Models;

[Table(TABLENAME, Schema = PowerPlantDbContext.SCHEMA)]
[Keyless]
public partial class RealProduction
{
    public const string TABLENAME = "real_productions";

    [JsonIgnore]
    [Column("power_plant_id", TypeName = "integer")]
    [ForeignKey("FK_power_plants_real_productions")]
    public int PowerPlantId { get; set; }

    [Column("power")]
    public double Power { get; set; } // GWh

    [Column("timestamp")]
    public DateTime Timestamp { get; set; }

    [JsonIgnore]
    public PowerPlant PowerPlant { get; set; }

    public static string Table() { return $"{PowerPlantDbContext.SCHEMA}.{TABLENAME}"; }

    public override string ToString() { return $"{PowerPlantId}, {Power}, {Timestamp}"; }
}


using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Uprise.Repository.Power_Plant.Models;

[Table(TABLENAME, Schema = PowerPlantDbContext.SCHEMA)]
public partial class PowerPlant
{
    public const string TABLENAME = "power_plants";

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Column("id", TypeName = "serial")]
    public int Id { get; set; }

    [MaxLength(100)]
    [Column("name", TypeName = "character varying(100)")]
    public string Name { get; set; } = "";

    [Column("installed_power", TypeName = "character varying")]
    [JsonPropertyName("installed_power")]
    public string InstalledPower { get; set; } = null!;

    [Column("date_of_installation")]
    [JsonPropertyName("date_of_installation")]
    public DateTime DateOfInstallation { get; set; } // 2023-11-01T10:56:45.9904633+01:00

    [Required]
    [Column("latitude")]
    public float Latitude { get; set; }

    [Required]
    [Column("longitude")]
    public float Longitude { get; set; }

    public static string Table() { return $"{PowerPlantDbContext.SCHEMA}.{TABLENAME}"; }

    public override string ToString()
    {
        return $"{Name}, {InstalledPower}, {Latitude}, {Longitude}, {DateOfInstallation}";
    }
}

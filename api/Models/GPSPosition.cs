using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GdzieBus.Api.Models;

public class GPSPosition
{
    [Key]
    public Guid PositionId { get; set; }

    [Required]
    public Guid VehicleId { get; set; }
    [ForeignKey(nameof(VehicleId))]
    public Vehicle? Vehicle { get; set; }

    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public double? SpeedKmh { get; set; }
    public double? DirectionDegrees { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public bool IsOnline { get; set; }
}

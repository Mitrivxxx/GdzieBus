using System;
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

    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public decimal? SpeedKmh { get; set; }
    public decimal? DirectionDegrees { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public bool IsOnline { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GdzieBus.Api.Models;

public class Schedule
{
    [Key]
    public Guid ScheduleId { get; set; }

    [Required]
    public Guid RouteId { get; set; }
    [ForeignKey(nameof(RouteId))]
    public Route? Route { get; set; }

    [Required]
    public Guid VehicleId { get; set; }
    [ForeignKey(nameof(VehicleId))]
    public Vehicle? Vehicle { get; set; }

    [Required]
    public Guid DriverId { get; set; }
    [ForeignKey(nameof(DriverId))]
    public Employee? Driver { get; set; }

    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }

    public string? RepeatPattern { get; set; }
    public DateTime? ValidFrom { get; set; }
    public DateTime? ValidUntil { get; set; }
    public string? Status { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public ICollection<Trip>? Trips { get; set; }
}

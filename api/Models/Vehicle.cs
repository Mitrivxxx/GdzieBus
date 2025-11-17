using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GdzieBus.Api.Models;

public class Vehicle
{
    [Key]
    public Guid VehicleId { get; set; }

    [Required]
    public string RegistrationNumber { get; set; } = null!;

    public string? Brand { get; set; }
    public string? Model { get; set; }
    public string? VehicleType { get; set; }
    public string? Status { get; set; }

    public string? GpsDeviceId { get; set; }

    public ICollection<Schedule>? Schedules { get; set; }
    public ICollection<GPSPosition>? GPSPositions { get; set; }
}

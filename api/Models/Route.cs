using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GdzieBus.Api.Models;

public class Route
{
    [Key]
    public Guid RouteId { get; set; }

    public string? RouteName { get; set; }

    public string? RouteCode { get; set; }

    public Guid? OriginStopId { get; set; }
    [ForeignKey(nameof(OriginStopId))]
    public Stop? OriginStop { get; set; }

    public Guid? DestinationStopId { get; set; }
    [ForeignKey(nameof(DestinationStopId))]
    public Stop? DestinationStop { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<RoutePattern>? RoutePatterns { get; set; }
    public ICollection<Schedule>? Schedules { get; set; }
    public ICollection<Notification>? Notifications { get; set; }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GdzieBus.Api.Models;

public class RoutePattern
{
    [Key]
    public Guid PatternId { get; set; }

    [Required]
    public Guid RouteId { get; set; }
    [ForeignKey(nameof(RouteId))]
    public Route? Route { get; set; }

    [Required]
    public Guid StopId { get; set; }
    [ForeignKey(nameof(StopId))]
    public Stop? Stop { get; set; }

    public int SequenceNumber { get; set; }
    public int? EstimatedTravelTime { get; set; }
    public decimal? EstimatedDistance { get; set; }
    public bool IsActive { get; set; } = true;
}

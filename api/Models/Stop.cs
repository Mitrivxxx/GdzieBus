using System.ComponentModel.DataAnnotations;

namespace GdzieBus.Api.Models;

public class Stop
{
    [Key]
    public Guid StopId { get; set; }

    [Required]
    public string? StopName { get; set; }

    public string? StopCode { get; set; }

    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }

    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Zone { get; set; }

    public ICollection<RoutePattern>? RoutePatterns { get; set; }
}

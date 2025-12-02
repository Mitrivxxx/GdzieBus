using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GdzieBus.Api.Models;

public class Trip
{
    [Key]
    public Guid TripId { get; set; }

    [Required]
    public Guid ScheduleId { get; set; }
    [ForeignKey(nameof(ScheduleId))]
    public Schedule? Schedule { get; set; }

    public DateTime? ActualStartTime { get; set; }
    public DateTime? ActualEndTime { get; set; }

    public string? Status { get; set; }
    public int? DelayMinutes { get; set; }
}

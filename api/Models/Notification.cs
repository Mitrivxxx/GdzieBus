using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GdzieBus.Api.Models;

public class Notification
{
    [Key]
    public Guid NotificationId { get; set; }

    [Required]
    public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }

    public string? Title { get; set; }
    public string? Message { get; set; }
    public string? Type { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid? RelatedTripId { get; set; }
    [ForeignKey(nameof(RelatedTripId))]
    public Trip? RelatedTrip { get; set; }

    public Guid? RelatedRouteId { get; set; }
    [ForeignKey(nameof(RelatedRouteId))]
    public Route? RelatedRoute { get; set; }
}

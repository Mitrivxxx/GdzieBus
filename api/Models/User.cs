using System.ComponentModel.DataAnnotations;

namespace GdzieBus.Api.Models;

public class User
{
    [Key]
    public Guid UserId { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string PasswordHash { get; set; } = null!;

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Employee>? Employees { get; set; }
    public ICollection<Notification>? Notifications { get; set; }
}

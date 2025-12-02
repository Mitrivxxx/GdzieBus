using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GdzieBus.Api.Models;

public class Employee
{
    [Key]
    public Guid EmployeeId { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }

    [Required]
    public string EmployeeNumber { get; set; } = null!;

    public DateTime? HireDate { get; set; }
    public string? LicenseNumber { get; set; }
    public DateTime? LicenseExpiryDate { get; set; }
    public string? Status { get; set; }

    public Guid? AssignedVehicleId { get; set; }
    [ForeignKey(nameof(AssignedVehicleId))]
    public Vehicle? AssignedVehicle { get; set; }
}

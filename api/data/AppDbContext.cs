using Microsoft.EntityFrameworkCore;
using GdzieBus.Api.Models;
using RouteModel = GdzieBus.Api.Models.Route;

namespace GdzieBus.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Vehicle> Vehicles { get; set; } = null!;
        public DbSet<Stop> Stops { get; set; } = null!;
        public DbSet<RouteModel> Routes { get; set; } = null!;
        public DbSet<RoutePattern> RoutePatterns { get; set; } = null!;
        public DbSet<Schedule> Schedules { get; set; } = null!;
        public DbSet<GPSPosition> GPSPositions { get; set; } = null!;
        public DbSet<Trip> Trips { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Indexes / constraints that reflect ERD intentions
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Employee>().HasIndex(e => e.EmployeeNumber).IsUnique();
            modelBuilder.Entity<Vehicle>().HasIndex(v => v.RegistrationNumber).IsUnique();
            modelBuilder.Entity<Vehicle>().HasIndex(v => v.GpsDeviceId).IsUnique();
            modelBuilder.Entity<Stop>().HasIndex(s => s.StopCode).IsUnique();
            modelBuilder.Entity<RouteModel>().HasIndex(r => r.RouteCode).IsUnique();

            // Relationships use EF conventions; add explicit FK mappings if desired
        }
    }

}

using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using GdzieBus.Api.Data;
using GdzieBus.Api.Models;
using GdzieBus.Api.DTOs;
using GdzieBus.Api.Services.Interfaces;
using GdzieBus.Api.Mappers;
using GdzieBus.Api.Hubs;

namespace GdzieBus.Api.Services.Implementations
{
    public class GpsPositionService : IGpsPositionService
    {
        private readonly AppDbContext _db;
        private readonly IHubContext<GpsHub> _hub;

        public GpsPositionService(AppDbContext db, IHubContext<GpsHub> hub)
        {
            _db = db;
            _hub = hub;
        }

        public async Task<GPSPosition> UpdateLastPositionAsync(GpsPositionDto dto)
        {
            Console.WriteLine($"[SERVICE] Processing update for Vehicle: {dto.VehicleId}");

            // 1. Sprawdź czy pojazd istnieje, jeśli nie - utwórz go automatycznie
            var vehicleExists = await _db.Vehicles.AnyAsync(v => v.VehicleId == dto.VehicleId);
            if (!vehicleExists)
            {
                Console.WriteLine($"[SERVICE] Vehicle {dto.VehicleId} not found. Creating new...");
                var newVehicle = new Vehicle
                {
                    VehicleId = dto.VehicleId,
                    RegistrationNumber = $"NEW-{dto.VehicleId.ToString().Substring(0, 6)}", // Generujemy tymczasowy numer
                    Status = "Active",
                    VehicleType = "Unknown"
                };
                _db.Vehicles.Add(newVehicle);
                await _db.SaveChangesAsync();
                Console.WriteLine($"[SERVICE] Vehicle created.");
            }

            // Pobierz ostatnią pozycję
            var last = await _db.GPSPositions
                .FirstOrDefaultAsync(v => v.VehicleId == dto.VehicleId);

            if (last == null)
            {
                Console.WriteLine($"[SERVICE] No existing GPS record. Creating new...");
                last = new GPSPosition
                {
                    PositionId = Guid.NewGuid(),
                    VehicleId = dto.VehicleId
                };

                _db.GPSPositions.Add(last);
            }
            else 
            {
                Console.WriteLine($"[SERVICE] Updating existing GPS record {last.PositionId}...");
            }

            // Nadpisz wartości
            last.Latitude = dto.Latitude;
            last.Longitude = dto.Longitude;
            last.SpeedKmh = dto.SpeedKmh;
            last.DirectionDegrees = dto.DirectionDegrees;
            last.Timestamp = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            Console.WriteLine($"[SERVICE] Saved to DB.");

            // Real-time push
            await _hub.Clients.All.SendAsync("gpsUpdate", new
            {
                vehicleId = last.VehicleId,
                latitude = last.Latitude,
                longitude = last.Longitude,
                speedKmh = last.SpeedKmh,
                directionDegrees = last.DirectionDegrees,
                timestamp = last.Timestamp
            });

            return last;
        }
    }
}
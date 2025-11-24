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

        public async Task<GPSPosition> AddPositionAsync(GpsPositionDto dto)
        {
            // 1. Walidacja czy istnieje pojazd
            var exists = await _db.Vehicles.AnyAsync(v => v.VehicleId == dto.VehicleId);
            if (!exists)
                throw new Exception("Vehicle does not exist.");

            // 2. Mapowanie DTO → Entity
            var entity = GpsMapper.ToEntity(dto);

            // 3. Zapis do bazy
            _db.GPSPositions.Add(entity);
            await _db.SaveChangesAsync();

            // 4. Emitowanie przez SignalR
            await _hub.Clients.All.SendAsync("gpsUpdate", new
            {
                vehicleId = entity.VehicleId,
                latitude = entity.Latitude,
                longitude = entity.Longitude,
                speedKmh = entity.SpeedKmh,
                directionDegrees = entity.DirectionDegrees,
                timestamp = entity.Timestamp
            });

            return entity;
        }
    }
}
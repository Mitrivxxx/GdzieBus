using GdzieBus.Api.DTOs;
using GdzieBus.Api.Models;

namespace GdzieBus.Api.Mappers
{
    public static class GpsMapper
    {
        public static GPSPosition ToEntity(GpsPositionDto dto)
        {
            return new GPSPosition
            {
                PositionId = Guid.NewGuid(),
                VehicleId = dto.VehicleId,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                SpeedKmh = dto.SpeedKmh,
                DirectionDegrees = dto.DirectionDegrees,
                Timestamp = DateTime.UtcNow,
                IsOnline = true
            };
        }
        public static GpsPositionDto ToDto(GPSPosition entity)
        {
            return new GpsPositionDto
            {
                VehicleId = entity.VehicleId,
                Latitude = entity.Latitude ?? 0,
                Longitude = entity.Longitude ?? 0,
                SpeedKmh = entity.SpeedKmh ?? 0,
                DirectionDegrees = entity.DirectionDegrees ?? 0,
                Timestamp = entity.Timestamp
            };
        }
    }
}
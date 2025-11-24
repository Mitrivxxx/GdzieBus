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
                Latitude = (decimal)dto.Latitude,
                Longitude = (decimal)dto.Longitude,
                SpeedKmh = (decimal)dto.SpeedKmh,
                DirectionDegrees = (decimal)dto.DirectionDegrees,
                Timestamp = DateTime.UtcNow,
                IsOnline = true
            };
        }
    }

}
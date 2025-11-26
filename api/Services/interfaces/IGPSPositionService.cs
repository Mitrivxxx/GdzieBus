using GdzieBus.Api.DTOs;
using GdzieBus.Api.Models;

namespace GdzieBus.Api.Services.Interfaces
{
    public interface IGpsPositionService
    {
        Task<GPSPosition> UpdateLastPositionAsync(GpsPositionDto dto);
        Task<GpsPositionDto?> GetLastPositionAsync(Guid vehicleId);
    }
}
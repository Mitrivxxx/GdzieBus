using GdzieBus.Api.DTOs;
using GdzieBus.Api.Models;

namespace GdzieBus.Api.Services.Interfaces
{
    public interface IGpsPositionService
    {
        Task<GPSPosition> AddPositionAsync(GpsPositionDto dto);
    }
}
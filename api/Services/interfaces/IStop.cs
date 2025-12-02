using GdzieBus.Api.DTOs;

namespace GdzieBus.Api.Services.Interfaces
{
    public interface IStop
    {
        Task<StopDto> AddStop(StopDto dto);
        Task<IEnumerable<StopDto>> GetAllStops();
        Task<StopDto> DeleteStop(string StopName);
    }
}
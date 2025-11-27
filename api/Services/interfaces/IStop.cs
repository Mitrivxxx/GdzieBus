using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs;

namespace api.Services.interfaces
{
    public interface IStop
    {
        Task<StopDto> AddStop(StopDto dto);
        Task<IEnumerable<StopDto>> GetAllStops();
    }
}
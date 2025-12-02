using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GdzieBus.Api.DTOs;
using GdzieBus.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GdzieBus.Api.Controllers
{
    [ApiController]
    [Route("api/stop")]
    public class StopController : ControllerBase
    {
        private readonly IStop _stopService;

        public StopController(IStop stopService)
        {
            _stopService = stopService;
        }

        [HttpPost]
        public async Task<IActionResult> AddStop(StopDto dto)
        {
            try
            {
                var stop = await _stopService.AddStop(dto);
                return Created($"/stops/{Uri.EscapeDataString(stop.StopName ?? string.Empty)}", stop);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("23505"))
                {
                    return Conflict($"Przystanek o kodzie '{dto.StopCode}' już istnieje.");
                }
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStops()
        {
            var stops = await _stopService.GetAllStops();
            return Ok(stops);
        }
    }
}
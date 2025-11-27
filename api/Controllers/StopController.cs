using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs;
using api.Services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
            var stop = await _stopService.AddStop(dto);
            return Created($"/stops/{stop.StopName}", stop);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStops()
        {
            var stops = await _stopService.GetAllStops();
            return Ok(stops);
        }
    }
}
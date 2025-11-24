using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GdzieBus.Api.DTOs;
using GdzieBus.Api.Data;
using GdzieBus.Api.Models;
using GdzieBus.Api.Services.Interfaces;

namespace GdzieBus.Api.Controllers
{
    [ApiController]
    [Route("api/gps")]
    public class GpsController : ControllerBase
    {
        private readonly IGpsPositionService _service;

        public GpsController(IGpsPositionService service)
        {
            _service = service;
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] GpsPositionDto dto)
        {
            var result = await _service.UpdateLastPositionAsync(dto);
            return Ok(new { status = "saved", vehicleId = result.VehicleId });
        }
    }
}
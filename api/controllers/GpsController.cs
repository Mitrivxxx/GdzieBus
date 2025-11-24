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

        [HttpPost]
        public async Task<IActionResult> AddPosition([FromBody] GpsPositionDto dto)
        {
            var result = await _service.AddPositionAsync(dto);
            return Ok(result.PositionId);
        }
    }

}
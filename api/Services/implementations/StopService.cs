using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GdzieBus.Api.Data;
using Microsoft.AspNetCore.SignalR;
using GdzieBus.Api.Services.Interfaces;
using GdzieBus.Api.Mappers;
using GdzieBus.Api.Hubs;
using GdzieBus.Api.Models;
using api.DTOs;
using api.Mappers;
using Microsoft.EntityFrameworkCore;

using api.Services.interfaces;

namespace api.Services.implementations
{
    public class StopService : IStop
    {
        private readonly AppDbContext _context;

        private readonly IHubContext<GpsHub> _hub;

        public StopService(AppDbContext context, IHubContext<GpsHub> hub)
        {
            _context = context;
            _hub = hub;
        }

        public async Task<StopDto> AddStop(StopDto dto)
        {
            var stop = StopMapper.ToEntity(dto);

            await _context.Stops.AddAsync(stop);
            await _context.SaveChangesAsync();

            return StopMapper.ToDto(stop);
        }

        public async Task<IEnumerable<StopDto>> GetAllStops()
        {
            var stops = await _context.Stops
                .AsNoTracking()
                .ToListAsync();

            return stops.Select(StopMapper.ToDto);
        }

    }
}
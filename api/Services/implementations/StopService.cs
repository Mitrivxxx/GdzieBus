using GdzieBus.Api.Data;
using GdzieBus.Api.Services.Interfaces;
using GdzieBus.Api.Mappers;
using GdzieBus.Api.DTOs;
using Microsoft.EntityFrameworkCore;


namespace GdzieBus.Api.Services.Implementations
{
    public class StopService : IStop
    {
        private readonly AppDbContext _context;

        public StopService(AppDbContext context)
        {
            _context = context;
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

        public async Task<StopDto> DeleteStop(string stopName)
        {
            var stop = await _context.Stops
                .FirstOrDefaultAsync(s => s.StopName == stopName);
            if (stop == null)
                throw new KeyNotFoundException($"Przystanek o nazwie '{stopName}' nieznaleziono.");
            _context.Stops.Remove(stop);
            await _context.SaveChangesAsync();

            return StopMapper.ToDto(stop);
        }
    }
}
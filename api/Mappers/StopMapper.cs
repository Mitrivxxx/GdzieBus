using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs;
using GdzieBus.Api.Models;

namespace api.Mappers
{
    public class StopMapper
    {
        public static StopDto ToDto(Stop stop)
        {
            return new StopDto
            {
                StopName = stop.StopName,
                StopCode = stop.StopCode,
                Latitude = stop.Latitude,
                Longitude = stop.Longitude,
                Address = stop.Address,
                City = stop.City,
                Zone = stop.Zone
            };
        }

        public static Stop ToEntity(StopDto dto)
        {
            return new Stop
            {
                StopName = dto.StopName,
                StopCode = dto.StopCode,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                Address = dto.Address,
                City = dto.City,
                Zone = dto.Zone
            };
        }
    }
}
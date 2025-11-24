using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace GdzieBus.Api.DTOs
{
    public class GpsPositionDto
    {
        public Guid VehicleId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public decimal SpeedKmh { get; set; }
        public decimal DirectionDegrees { get; set; }
    }
}
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

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double SpeedKmh { get; set; }

        public double DirectionDegrees { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
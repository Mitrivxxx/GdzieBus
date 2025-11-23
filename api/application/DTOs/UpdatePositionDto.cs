using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace GdzieBus.Api.Application.DTOs
{
    public class UpdatePositionDto
    {
        public Guid VehicleId { get; set; }

        // Mapowanie JSON "lat" -> Latitude
        [JsonPropertyName("lat")]
        public double Latitude { get; set; }

        // Mapowanie JSON "lng" -> Longitude
        [JsonPropertyName("lng")]
        public double Longitude { get; set; }

        public double SpeedKmh { get; set; }
        public double DirectionDegrees { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
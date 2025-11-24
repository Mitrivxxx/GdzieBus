using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace GdzieBus.Api.DTOs
{
    public class GpsPositionDto
    {
        [JsonPropertyName("vehicleId")]
        public Guid VehicleId { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("speedKmh")]
        public double SpeedKmh { get; set; }

        [JsonPropertyName("directionDegrees")]
        public double DirectionDegrees { get; set; }
        
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
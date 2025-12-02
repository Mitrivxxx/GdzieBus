using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GdzieBus.Api.DTOs
{
    public class StopDto
    {
        public string? StopName { get; set; } = null!;
        public string? StopCode { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Zone { get; set; }
    }
}
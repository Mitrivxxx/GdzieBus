using Microsoft.AspNetCore.Mvc;

namespace GdzieBus.Api.Controllers
{
    [ApiController]
    [Route("api/gps-test")]
    public class TestGpsController : ControllerBase
    {
        [HttpPost]
        public IActionResult ReceiveGps([FromBody] GpsPositionTest position)
        {
            Console.WriteLine($"GPS RECEIVED: {position.Lat}, {position.Lng}");
            return Ok(new { status = "received" });
        }
         [HttpGet]
        public IActionResult Ping()
        {
            return Ok("OK");
        }
    }

    public record GpsPositionTest(double Lat, double Lng);
}
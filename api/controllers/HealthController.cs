using Microsoft.AspNetCore.Mvc;
using Npgsql;

[ApiController]
[Route("health")]
public class HealthController : ControllerBase
{
    private readonly IConfiguration _config;
    public HealthController(IConfiguration config)
    {
        _config = config;
    }

    [HttpGet("full")]
    public async Task<IActionResult> FullCheck()
    {
        var connStr = _config.GetConnectionString("DefaultConnection");

        await using var conn = new NpgsqlConnection(connStr);
        await conn.OpenAsync();

        await using var cmd = new NpgsqlCommand("SELECT 1", conn);
        var result = await cmd.ExecuteScalarAsync();

        return Ok(new { status = "ok", db = result });
    }

}

using Microsoft.EntityFrameworkCore;
using GdzieBus.Api.Data;
using GdzieBus.Api.Hubs;
using Microsoft.AspNetCore.HttpOverrides;
using GdzieBus.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAppServices();
builder.Services.AddSignalR();
builder.Services.AddHealthChecks();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseNpgsql(connectionString, npgsqlOptions => 
    npgsqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)));

var app = builder.Build();

app.UseForwardedHeaders();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.MapControllers();
app.MapHub<GpsHub>("/gpsHub");

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();



app.Run();
public partial class Program { }

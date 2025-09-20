using Microsoft.EntityFrameworkCore;
using minimal_api.Domain.DTOs;
using minimal_api.Domain.Entities;
using minimal_api.Domain.Interfaces;
using minimal_api.Domain.ModelsViews;
using minimal_api.Domain.Services;
using minimal_api.Infraestructure.Db;
using System.Xml.Linq;

#region Builder 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAdmnistratorService, AdmnistratorService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DB context using connection string from configuration
var mysqlConnection = builder.Configuration.GetConnectionString("mysql");
if (!string.IsNullOrEmpty(mysqlConnection))
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseMySql(mysqlConnection, ServerVersion.AutoDetect(mysqlConnection)));
}
else
{
    // If no connection string is present, still register a DbContext with default options so EF tools can find it.
    builder.Services.AddDbContext<ApplicationDbContext>();
}

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

#endregion

#region Home
app.MapGet("/", () => Results.Json(new Home())).WithTags("Home");
#endregion

#region Administrators              
app.MapPost("admnistrators/login", (LoginDTO loginDTO, IAdmnistratorService admnistratorService) =>
{
    var admin = admnistratorService.Login(loginDTO);

    if (admin != null)
    {
        return Results.Ok("Login successfully");
    }
    else
    {
        return Results.Unauthorized();
    }
}).WithTags("Admnistrators");
#endregion

#region Vehicles

app.MapPost("/vehicles", (VehicleDTO vehicleDTO, IVehicleService vehicleService) =>
{

    var vehicle = new Vehicle
    {
        Name = vehicleDTO.Name,
        Brand = vehicleDTO.Brand,
        Year = vehicleDTO.Year
    };

    vehicleService.Insert(vehicle);

    return Results.Created($"/vehicle/{vehicle.Id}", vehicle);
}).WithTags("Vehicles");

app.MapGet("/vehicles", (int? page, IVehicleService vehicleService) =>
{

    var vehicles = vehicleService.All(page);

    return Results.Ok(vehicles);
}).WithTags("Vehicles");

app.MapGet("/vehicles/{id}", (int id, IVehicleService vehicleService) =>
{
    var vehicle = vehicleService.GetById(id);

    if (vehicle == null) return Results.NotFound();

    return Results.Ok(vehicle);
}).WithTags("Vehicles");

app.MapPut("/vehicles/{id}", (int id,  VehicleDTO vehicleDTO, IVehicleService vehicleService) =>
{
    var vehicle = vehicleService.GetById(id);

    if (vehicle == null) return Results.NotFound();

    vehicle.Name = vehicleDTO.Name;
    vehicle.Brand = vehicleDTO.Brand;
    vehicle.Year = vehicleDTO.Year;

    vehicleService.Update(vehicle);

    return Results.Ok(vehicle);
}).WithTags("Vehicles");

app.MapDelete("/vehicles/{id}", (int id, IVehicleService vehicleService) =>
{
    var vehicle = vehicleService.GetById(id);

    if (vehicle == null) return Results.NotFound();


    vehicleService.Delete(vehicle);

    return Results.NoContent();
}).WithTags("Vehicles");

#endregion


#region App

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion

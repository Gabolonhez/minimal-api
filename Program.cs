using Microsoft.EntityFrameworkCore;
using minimal_api.Domain.DTOs;
using minimal_api.Domain.Entities;
using minimal_api.Domain.Interfaces;
using minimal_api.Domain.ModelsViews;
using minimal_api.Domain.Services;
using minimal_api.Infraestructure.Db;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

#region Builder 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAdministratorService, AdministratorService>();
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
app.MapPost("administrators/login", (LoginDTO loginDTO, IAdministratorService administratorService) =>
{
    var admin = administratorService.Login(loginDTO);

    if (admin != null)
    {
        return Results.Ok("Login successfully");
    }
    else
    {
        return Results.Unauthorized();
    }
}).WithTags("Admnistrators");

app.MapGet("administrators/All", (int? page, IAdministratorService administratorService) =>
{

    var adms = new List<AdministratorModelView>();

    var administrators = administratorService.All(page);

    foreach (var adm in administrators)
    {
        adms.Add(new AdministratorModelView
        {
            Id = adm.Id,
            Email = adm.Email,
            Profile = adm.Profile
        });
    }

    return Results.Ok(administrators);
}).WithTags("Administrators");

app.MapGet("administrators/{id}", (int id, IAdministratorService administratorService) =>
{

    var administrator = administratorService.FindById(id);

    if (administrator == null)
        return Results.NotFound();


    return Results.Ok(new AdministratorModelView
    {
        Id = administrator.Id,
        Email = administrator.Email,
        Profile = administrator.Profile
    });
}).WithTags("Administrators");

app.MapPost("administrators/", (AdministratorDTO administratorDTO, IAdministratorService administratorService) =>
{
    var validation = new ValidationErrors
    {
        Messages = new List<string>()
    };

    if (string.IsNullOrEmpty(administratorDTO.Email))
        validation.Messages.Add("Email can not be empty");

    if (string.IsNullOrEmpty(administratorDTO.Password))
        validation.Messages.Add("Email can not be empty");

    if (administratorDTO.Profile == null)
        validation.Messages.Add("Email can not be empty");

    if (validation.Messages.Count > 0)
        return Results.BadRequest(validation);

    var administrator = new Administrator
    {
        Email = administratorDTO.Email,
        Password = administratorDTO.Password,
        Profile = administratorDTO.Profile?.ToString()
    };

    administratorService.Insert(administrator);
    return Results.Created("$/Administrator/{administrator.Id}", new AdministratorModelView
    {
        Id = administrator.Id,
        Email = administrator.Email,
        Profile = administrator.Profile
    });
   
}).WithTags("Admnistrators");

#endregion

#region Vehicles

ValidationErrors validateDTO(VehicleDTO vehicleDTO)
{
    var validation = new ValidationErrors
    {
        Messages  = new List<string>()
    };


    if (string.IsNullOrEmpty(vehicleDTO.Name))
        validation.Messages.Add("The vehicle name can not be empty");

    if (string.IsNullOrEmpty(vehicleDTO.Brand))
        validation.Messages.Add("The vehicle brand can not be empty");

    if (vehicleDTO.Year < 1950)
        validation.Messages.Add("The vehicle year can not be less than 1950, is too old");

    return validation;
}

app.MapPost("/vehicles", (VehicleDTO vehicleDTO, IVehicleService vehicleService) =>
{
   var validation = validateDTO(vehicleDTO);

    if (validation.Messages.Count > 0)
        return Results.BadRequest(validation);


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

    var validation = validateDTO(vehicleDTO);

    if (validation.Messages.Count > 0)
        return Results.BadRequest(validation);
 

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

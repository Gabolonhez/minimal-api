using Microsoft.EntityFrameworkCore;
using minimal_api.Infraestructure.Db;
using minimal_api.Domain.Services;
using minimal_api.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAdmnistratorService, AdmnistratorService>();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapPost("/login", (LoginDTO loginDTO, IAdmnistratorService admnistratorService) =>
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
});


app.Run();

public class LoginDTO
{
    public string Email { get; set; } = default!;

    public string Password { get; set; } = default!;
}
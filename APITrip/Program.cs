using APITrip.Entities;
using APITrip.Helpers;
using APITrip.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("TestDb")));

// Ajouter la politique CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // URL de ton frontend Vue
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

services.AddControllers().AddJsonOptions(x =>
{
    // serialize enums as strings in api responses (e.g. Role)
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    // ignore omitted parameters on models to enable optional params (e.g.User update)
    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    // Convertir les propriétés en camelCase dans le JSON
    x.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
});

services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// configure DI for application services
services.AddScoped<IUserService, UserService>();
services.AddScoped<IAgenceService, AgenceService>();
services.AddScoped<IChauffeurService, ChauffeurService>();
services.AddScoped<IClientService, ClientService>();
services.AddScoped<IGestionnaireService, GestionnaireService>();
services.AddScoped<IOffreService, OffreService>();
services.AddScoped<IReservationService, ReservationService>();
services.AddScoped<IFlotteService, FlotteService>();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Activer la politique CORS ici
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();

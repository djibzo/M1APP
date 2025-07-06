using APITrip.Auth; 
using APITrip.Helpers;
using APITrip.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer; 
using Microsoft.IdentityModel.Tokens;
using NLog.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Text.Json;
using System.Security.Claims;
var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// --- Configuration des Services ---

// NLog Logging
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.SetMinimumLevel(LogLevel.Trace);
    logging.AddNLog();
});

// DbContexts
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("TestDb")));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("TestDb")));

// Controllers et JSON Options
builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
   .AddEntityFrameworkStores<ApplicationDbContext>()
   .AddDefaultTokenProviders();

// --- Configuration Keycloak (Auth JWT Bearer) ---
var keycloakOptions = builder.Configuration.GetSection("Keycloak");
var authServerUrl = keycloakOptions["AuthServerUrl"];
var realm = keycloakOptions["Realm"];
var audience = keycloakOptions["Audience"];
var metadataAddress = keycloakOptions["MetadataAddress"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; 
})
.AddJwtBearer(options =>
{
    var keycloakOptions = builder.Configuration.GetSection("Keycloak");
    var authServerUrl = keycloakOptions["AuthServerUrl"];
    var realm = keycloakOptions["Realm"];
    var audience = keycloakOptions["Audience"];
    var metadataAddress = keycloakOptions["MetadataAddress"];

    options.Authority = $"{authServerUrl}/realms/{realm}";
    options.Audience = audience;
    options.MetadataAddress = metadataAddress;
    options.RequireHttpsMetadata = string.Equals(keycloakOptions["SslRequired"], "none", StringComparison.OrdinalIgnoreCase) ? false : true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateLifetime = true,
    };
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateLifetime = true,
    };
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("Authentication failed: " + context.Exception.Message);
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            context.HandleResponse();
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync("{\"error\":\"unauthorized\",\"message\":\"Authentication required or token is invalid.\"}");
        },
        OnForbidden = context =>
        {
            context.Response.StatusCode = 403;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync("{\"error\":\"forbidden\",\"message\":\"You do not have permission to access this resource.\"}");
        },
        OnTokenValidated = context =>
        {
            Console.WriteLine("Token validated!");

            // --- Logique pour extraire et ajouter les rôles Keycloak aux claims de l'utilisateur ---
            var claimsIdentity = context.Principal.Identity as ClaimsIdentity;

            if (claimsIdentity != null)
            {
                // Extraire les rôles de royaume (Realm Roles)
                var realmAccessClaim = context.Principal.FindFirst("realm_access")?.Value;
                if (!string.IsNullOrEmpty(realmAccessClaim))
                {
                    try
                    {
                        using JsonDocument doc = JsonDocument.Parse(realmAccessClaim);
                        if (doc.RootElement.TryGetProperty("roles", out JsonElement rolesElement) && rolesElement.ValueKind == JsonValueKind.Array)
                        {
                            foreach (JsonElement role in rolesElement.EnumerateArray())
                            {
                                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.GetString()));
                            }
                        }
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"Error parsing realm_access claim: {ex.Message}");
                    }
                }

                // Extraire les rôles de client (Client Roles) spécifiques à votre API
                // Assurez-vous que "ma-super-api" correspond au Client ID que vous avez défini dans Keycloak.
                var resourceAccessClaim = context.Principal.FindFirst("resource_access")?.Value;
                if (!string.IsNullOrEmpty(resourceAccessClaim))
                {
                    try
                    {
                        using JsonDocument doc = JsonDocument.Parse(resourceAccessClaim);
                        if (doc.RootElement.TryGetProperty("ma-super-api", out JsonElement clientRolesElement) && clientRolesElement.ValueKind == JsonValueKind.Object)
                        {
                            if (clientRolesElement.TryGetProperty("roles", out JsonElement rolesArray) && rolesArray.ValueKind == JsonValueKind.Array)
                            {
                                foreach (JsonElement role in rolesArray.EnumerateArray())
                                {
                                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.GetString()));
                                }
                            }
                        }
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"Error parsing resource_access claim: {ex.Message}");
                    }
                }
            }
          

            return Task.CompletedTask; 
        }
    };
});

builder.Services.AddAuthorization(); 

// CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
        });
});

// Swagger/OpenAPI 
builder.Services.AddEndpointsApiExplorer(); // Doit être avant AddSwaggerGen
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "APITrip", Version = "v1" });

    // Ajout de la définition de sécurité Bearer pour Swagger UI
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Entrez votre jeton JWT. Exemple : 12345abcdef",
    });

    // Indiquer à Swagger d'utiliser la sécurité Bearer pour toutes les opérations
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAgenceService, AgenceService>();
builder.Services.AddScoped<IChauffeurService, ChauffeurService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IGestionnaireService, GestionnaireService>();
builder.Services.AddScoped<IOffreService, OffreService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IFlotteService, FlotteService>();


// --- Configuration du pipeline de requêtes (Middleware) ---
var app = builder.Build();

app.UseCors("AllowReact"); 

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); // Mapper les requêtes aux contrôleurs

app.Run();
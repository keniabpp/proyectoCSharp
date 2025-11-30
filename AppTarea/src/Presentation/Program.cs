using Microsoft.EntityFrameworkCore;


using Application;
using Presentation.Middleware;
using Presentation.Security;
using AppTarea.Infrastructure;
using AppTarea.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
// using AppTarea.Infrastructure.Persistence.SeedData;





var builder = WebApplication.CreateBuilder(args);

//Configuración local + secretos del Secret Manager
builder.Configuration
.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
.AddEnvironmentVariables()
.AddUserSecrets<Program>();

// Configurar servicios de infraestructura (incluye Identity)
builder.Services.AddInfrastructure(builder.Configuration);

// Configurar JWT Authentication (mantener la configuración existente)
builder.Services.AddJwtAuthentication(builder.Configuration);

//  CORS configurado
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // frontend Angular
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});


// Servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AppTarea API", Version = "v1" });
    
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT: Bearer {token}"
    });

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
            Array.Empty<string>()
        }
    });
});




//Inyección de dependencias de infraestructura (incluye DbContext)
// Ya no necesitamos llamar AddInfrastructure aquí porque ya se llamó arriba
builder.Services.AddApplication();

var app = builder.Build();

// Asegurarse de que las tablas de Identity estén creadas
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppTarea.Infrastructure.Persistence.Context.AppDbContext>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
        
        // Aplicar migraciones pendientes
        await context.Database.MigrateAsync();
        
        // Ejecutar seed data para crear roles por defecto
        await AppTarea.Infrastructure.Persistence.Context.SeedData.SeedAsync(context, userManager, roleManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}



//Middleware
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}


app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication(); // primero
app.UseAuthorization();  // después

app.MapControllers();

app.Run();

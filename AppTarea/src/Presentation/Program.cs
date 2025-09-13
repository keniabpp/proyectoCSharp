

using Application;
using Presentation.Middleware;
using Presentation.Security;
using AppTarea.Infrastructure;





var builder = WebApplication.CreateBuilder(args);

//Configuración local + secretos del Secret Manager
builder.Configuration
.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
.AddUserSecrets<Program>();



builder.Services.AddJwtAuthentication(builder.Configuration);

// 👇 CORS configurado
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // frontend Angular
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


// Servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Solo ingrese el token JWT sin las comillas."
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});




//Inyección de dependencias de infraestructura (incluye DbContext)
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();




var app = builder.Build();



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

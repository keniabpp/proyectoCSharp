using Domain.Interfaces;
using Domain.Entities;
using Application.Interfaces;
using AppTarea.Infrastructure.Identity;
using AppTarea.Infrastructure.Persistence.Context;
using AppTarea.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace AppTarea.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Configurar DbContext

            var connStr = configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine($"[DEBUG] Cadena de conexión usada: {connStr}");
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    connStr,
                    sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,             // Número máximo de reintentos
                        maxRetryDelay: TimeSpan.FromSeconds(10), // Tiempo máximo de espera entre reintentos
                        errorNumbersToAdd: null       //null para los errores transitorios por defecto
                    )
                )
            );

            // Configurar ASP.NET Core Identity con seguridad completa
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                // Configuración de contraseñas - SEGURIDAD MÁXIMA
                options.Password.RequireDigit = true;              // Requiere números
                options.Password.RequireLowercase = true;          // Requiere minúsculas
                options.Password.RequireUppercase = true;          // Requiere mayúsculas
                options.Password.RequireNonAlphanumeric = true;    // Requiere caracteres especiales (@, #, $, etc.)
                options.Password.RequiredLength = 8;               // Mínimo 8 caracteres
                options.Password.RequiredUniqueChars = 4;          // Al menos 4 caracteres únicos

                // Configuración de usuario
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

                // Configuración de lockout - PROTECCIÓN CONTRA ATAQUES
                options.Lockout.AllowedForNewUsers = true;              // Activar para nuevos usuarios
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);  // 15 minutos bloqueado
                options.Lockout.MaxFailedAccessAttempts = 5;            // 5 intentos fallidos

                // Configuración de email y confirmación - ACTIVAR EN PRODUCCIÓN
                options.SignIn.RequireConfirmedEmail = true;            // Requiere email confirmado
                options.SignIn.RequireConfirmedPhoneNumber = false;     // Opcional: confirmar teléfono
                options.SignIn.RequireConfirmedAccount = true;          // Requiere cuenta confirmada

                // Tokens de seguridad
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            // Registrar servicios
            // El nuevo servicio híbrido que maneja tanto Identity como el patrón repository
            services.AddScoped<IApplicationUserService, ApplicationUserService>();
            
            // Mantener el repositorio original para compatibilidad (delegando al nuevo servicio)
            services.AddScoped<IUsuarioRepository>(provider => 
                provider.GetRequiredService<IApplicationUserService>());

            // Servicio para obtener información de roles
            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<ITableroRepository, TableroRepository>();
            services.AddScoped<IColumnaRepository, ColumnaRepository>();
            services.AddScoped<ITareaRepository, TareaRepository>();

            return services;
        }
    }
}

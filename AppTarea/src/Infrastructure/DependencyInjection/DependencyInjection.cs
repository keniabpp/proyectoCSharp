using Domain.Interfaces;
using AppTarea.Infrastructure.Persistence.Context;
using AppTarea.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace AppTarea.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,             // Número máximo de reintentos
                        maxRetryDelay: TimeSpan.FromSeconds(10), // Tiempo máximo de espera entre reintentos
                        errorNumbersToAdd: null       // Puedes dejar null para los errores transitorios por defecto
                    )
                )
            );

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ITableroRepository, TableroRepository>();
            services.AddScoped<IColumnaRepository, ColumnaRepository>();
            services.AddScoped<ITareaRepository, TareaRepository>();

            return services;
        }
    }
}

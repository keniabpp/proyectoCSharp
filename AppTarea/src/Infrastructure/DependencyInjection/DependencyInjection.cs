
using Domain.Interfaces;
using AppTarea.Infrastructure.Persistence.Context;
using AppTarea.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
 // Aquí está ApplicationUser



namespace AppTarea.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ITableroRepository, TableroRepository>();
            services.AddScoped<IColumnaRepository, ColumnaRepository>();
            services.AddScoped<ITareaRepository, TareaRepository>();


             

   
            
            
            return services;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AppTarea.Infrastructure.Persistence.Context;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        
        var connectionString = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION")
                               ?? "Server=localhost;Database=Tarea;Integrated Security=True;TrustServerCertificate=True;";
        
        optionsBuilder.UseSqlServer(connectionString);
        
        return new AppDbContext(optionsBuilder.Options);
    }
}
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace AppTarea.Infrastructure.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Tablero> Tableros { get; set; }
        public DbSet<Columna> Columnas { get; set; }
        public DbSet<Tarea> Tareas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación entre Usuario y Rol
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany()
                .HasForeignKey(u => u.id_rol)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación entre Tablero y Usuario
            modelBuilder.Entity<Tablero>()
                .HasOne(t => t.usuario) // Un Tablero tiene un Usuario
                .WithMany(u => u.Tableros) // Un Usuario puede tener muchos Tableros
                .HasForeignKey(t => t.creado_por) // La clave externa en Tablero es "creado_por"
                .OnDelete(DeleteBehavior.Restrict);

            // Relación entre Tablero y Rol
            modelBuilder.Entity<Tablero>()
              .HasOne(t => t.rol) // Un Tablero tiene un Rol
              .WithMany()         // Un Rol puede estar en muchos Tableros 
              .HasForeignKey(t => t.id_rol) // La clave externa en Tablero es "id_rol"
              .OnDelete(DeleteBehavior.Restrict); 

            // Relación entre Tablero y Columnas
            modelBuilder.Entity<Tablero>()
                .HasMany(t => t.columnas)
                .WithOne(c => c.tablero)
                .HasForeignKey(c => c.id_tablero)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración de la propiedad 'posicion' de Columnas
            modelBuilder.Entity<Columna>()
                .Property(c => c.posicion)
                .HasConversion<int>(); // Aquí dices que se guarde como int en BD

            // Relación entre Tarea y Usuario (creado_por)
            modelBuilder.Entity<Tarea>()
                .HasOne(t => t.creador)
                .WithMany(u => u.tareas_creadas)
                .HasForeignKey(t => t.creado_por)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación entre Tarea y Usuario (asignado_a)
            modelBuilder.Entity<Tarea>()
                .HasOne(t => t.asignado)
                .WithMany(u => u.tareas_asignadas)
                .HasForeignKey(t => t.asignado_a)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación entre Tarea y Tablero
            modelBuilder.Entity<Tarea>()
               .HasOne(t => t.tablero)
               .WithMany(tb => tb.tareas)
               .HasForeignKey(t => t.id_tablero)
               .OnDelete(DeleteBehavior.Restrict);

            // Relación entre Tarea y Columna
            modelBuilder.Entity<Tarea>()
               .HasOne(t => t.columna)
               .WithMany(c => c.tareas)
               .HasForeignKey(t => t.id_columna)
               .OnDelete(DeleteBehavior.Restrict);

            
        }
        
    }
}

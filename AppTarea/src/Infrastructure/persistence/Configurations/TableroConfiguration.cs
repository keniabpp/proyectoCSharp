using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace AppTarea.Infrastructure.Persistence.Configurations
{
    public class TableroConfiguration : IEntityTypeConfiguration<Tablero>
    {
        public void Configure(EntityTypeBuilder<Tablero> builder)
        {
            //Relación entre Tablero y Usuario

            builder.HasOne(t => t.usuario) // Un Tablero tiene un Usuario
            .WithMany(u => u.Tableros) // Un Usuario puede tener muchos Tableros
            .HasForeignKey(t => t.creado_por) // La clave externa en Tablero es "creado_por"
            .OnDelete(DeleteBehavior.Restrict);

            // Relación con Rol
            builder.HasOne(t => t.rol)
            .WithMany()
            .HasForeignKey(t => t.id_rol)
            .OnDelete(DeleteBehavior.Restrict);
                


            // Relación con Tareas
            builder.HasMany(t => t.tareas)
            .WithOne(tr => tr.tablero)
            .HasForeignKey(tr => tr.id_tablero)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
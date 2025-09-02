using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace AppTarea.Infrastructure.Persistence.Configurations
{
    public class TareaConfiguration : IEntityTypeConfiguration<Tarea>
    {
        public void Configure(EntityTypeBuilder<Tarea> builder)
        {
            // Relación con Usuario (creador)
            builder.HasOne(t => t.creador)
                   .WithMany(u => u.tareas_creadas)
                   .HasForeignKey(t => t.creado_por)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relación con Usuario (asignado)
            builder.HasOne(t => t.asignado)
                   .WithMany(u => u.tareas_asignadas)
                   .HasForeignKey(t => t.asignado_a)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relación con Tablero
            builder.HasOne(t => t.tablero)
                   .WithMany(tb => tb.tareas)
                   .HasForeignKey(t => t.id_tablero)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relación con Columna
            builder.HasOne(t => t.columna)
                   .WithMany(c => c.tareas)
                   .HasForeignKey(t => t.id_columna)
                   .OnDelete(DeleteBehavior.Restrict);

            
        }
    }
}

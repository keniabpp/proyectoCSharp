using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace AppTarea.Infrastructure.Persistence.Configurations
{
    public class TareaConfiguration : IEntityTypeConfiguration<Tarea>
    {
        public void Configure(EntityTypeBuilder<Tarea> builder)
        {
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

            // No se configuran relaciones con Usuario para mantener la pureza del dominio
            // Solo se usan IDs (creado_por, asignado_a) para referenciar AspNet Identity
        }
    }
}

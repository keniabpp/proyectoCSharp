using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace AppTarea.Infrastructure.Persistence.Configurations
{
    public class ColumnaConfiguration : IEntityTypeConfiguration<Columna>
    {
        public void Configure(EntityTypeBuilder<Columna> builder)
        {
            // Relación: cada columna puede tener muchas tareas
            builder.HasMany(c => c.tareas)
                .WithOne(t => t.columna)
                .HasForeignKey(t => t.id_columna)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de la propiedad 'posicion'
            builder.Property(c => c.posicion)
                .HasConversion<int>(); // Se guarda como int en la BD
        }
    }
}
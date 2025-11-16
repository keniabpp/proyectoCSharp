using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace AppTarea.Infrastructure.Persistence.Configurations
{
    public class TableroConfiguration : IEntityTypeConfiguration<Tablero>
    {
        public void Configure(EntityTypeBuilder<Tablero> builder)
        {
            // creado_por es solo un campo de referencia a AspNetUsers (sin navegación para mantener Domain limpio)
            // id_rol es solo un campo de referencia a AspNetRoles (sin navegación para mantener Domain limpio)
            
            // Relación con Tareas
            builder.HasMany(t => t.tareas)
            .WithOne(tr => tr.tablero)
            .HasForeignKey(tr => tr.id_tablero)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
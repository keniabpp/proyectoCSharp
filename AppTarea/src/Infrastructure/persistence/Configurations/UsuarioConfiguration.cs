using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace AppTarea.Infrastructure.Persistence.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            // Relación con Rol
            builder.HasOne(u => u.Rol)
                   .WithMany()
                   .HasForeignKey(u => u.id_rol)
                   .OnDelete(DeleteBehavior.Restrict);

            //Relación con Tableros (creados por el usuario)
            builder.HasMany(u => u.Tableros)
                   .WithOne(t => t.usuario)
                   .HasForeignKey(t => t.creado_por)
                   .OnDelete(DeleteBehavior.Restrict);

            //Relación con Tareas creadas
            builder.HasMany(u => u.tareas_creadas)
                   .WithOne(t => t.creador)
                   .HasForeignKey(t => t.creado_por)
                   .OnDelete(DeleteBehavior.Restrict);

            //Relación con Tareas asignadas
            builder.HasMany(u => u.tareas_asignadas)
                   .WithOne(t => t.asignado)
                   .HasForeignKey(t => t.asignado_a)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

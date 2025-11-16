using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AppTarea.Infrastructure.Identity;

namespace AppTarea.Infrastructure.Persistence.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("ApplicationUsers");
            
            builder.Property(u => u.Nombre)
                .HasMaxLength(100)
                .IsRequired();
                
            builder.Property(u => u.Apellido)
                .HasMaxLength(100)
                .IsRequired();
                
            builder.Property(u => u.Telefono)
                .HasMaxLength(20);

            // Relación con CustomRole
            builder.HasOne(u => u.CustomRole)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.CustomRoleId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
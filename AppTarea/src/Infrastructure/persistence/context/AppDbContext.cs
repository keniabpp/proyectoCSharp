using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Domain.Entities;
using AppTarea.Infrastructure.Identity;

namespace AppTarea.Infrastructure.Persistence.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Mantener las entidades existentes para compatibilidad
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> RolesCustom { get; set; } // Renombrado para evitar conflicto con Identity
        public DbSet<Tablero> Tableros { get; set; }
        public DbSet<Columna> Columnas { get; set; }
        public DbSet<Tarea> Tareas { get; set; }

        // Nuevas entidades de Identity (Infrastructure)
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configurar las tablas de Identity con nombres personalizados
            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUsers");
            modelBuilder.Entity<ApplicationRole>().ToTable("ApplicationRoles");
            modelBuilder.Entity<Microsoft.AspNetCore.Identity.IdentityUserRole<int>>().ToTable("ApplicationUserRoles");
            modelBuilder.Entity<Microsoft.AspNetCore.Identity.IdentityUserClaim<int>>().ToTable("ApplicationUserClaims");
            modelBuilder.Entity<Microsoft.AspNetCore.Identity.IdentityUserLogin<int>>().ToTable("ApplicationUserLogins");
            modelBuilder.Entity<Microsoft.AspNetCore.Identity.IdentityUserToken<int>>().ToTable("ApplicationUserTokens");
            modelBuilder.Entity<Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>>().ToTable("ApplicationRoleClaims");

            // Configurar relaciones de ApplicationUser
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.CustomRole)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.CustomRoleId)
                .OnDelete(DeleteBehavior.SetNull);

            // Aplica todas las configuraciones desde la carpeta Configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
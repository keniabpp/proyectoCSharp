using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AppTarea.Infrastructure.Identity;

namespace AppTarea.Infrastructure.Persistence.Context
{
    public static class SeedData
    {
        public static async Task SeedAsync(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            // Asegurar que la base de datos existe
            await context.Database.EnsureCreatedAsync();

            // Seed de Roles predeterminados en Identity
            await SeedRolesAsync(roleManager);
            
            // Seed de Usuario administrador (opcional)
            await SeedAdminUserAsync(userManager, roleManager);
        }

        private static async Task SeedRolesAsync(RoleManager<ApplicationRole> roleManager)
        {
            // Roles predeterminados
            string[] roleNames = { "Admin", "Usuario" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var role = new ApplicationRole(roleName);
                    await roleManager.CreateAsync(role);
                }
            }
        }

        private static async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            // Crear usuario administrador por defecto si no existe
            var adminEmail = "admin@apptarea.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    Nombre = "Administrador",
                    Apellido = "Sistema",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}

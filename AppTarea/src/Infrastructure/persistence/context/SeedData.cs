// using AppTarea.Infrastructure.Persistence.Context;
// using Domain.Entities;
// using Domain.Enums;

// namespace AppTarea.Infrastructure.Persistence.SeedData
// {
//     public static class DbInitializer
//     {
//         public static void SeedRoles(AppDbContext context)
//         {
//             if (!context.Roles.Any())
//             {
//                 context.Roles.AddRange(
//                     new Rol { nombre = "Admin" },
//                     new Rol { nombre = "Usuario" }
//                 );
//                 context.SaveChanges();
//             }
//         }

        


       

//         public static void SeedColumnas(AppDbContext context)
//         {
//             if (!context.Columnas.Any())
//             {
//                 context.Columnas.AddRange(
//                     new Columna
//                     {
//                         nombre = "Por hacer",
//                         posicion = EstadoColumna.PorHacer
//                     },
//                     new Columna
//                     {
//                         nombre = "En progreso",
//                         posicion = EstadoColumna.EnProgreso
//                     },
//                     new Columna
//                     {
//                         nombre = "Hecho",
//                         posicion = EstadoColumna.Hecho
//                     }
//                 );

//                 context.SaveChanges();
//             }
//         }


       

//     }
// }

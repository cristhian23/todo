using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoApi.Models;

namespace TodoApi.Data;

public static class DbInitializer
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TodoDbContext>();

        // Aplica migraciones
        context.Database.Migrate();

        // Seed usuario admin si no existe
        var adminEmail = "admin@example.com";
        var adminUser = context.Users.FirstOrDefault(u => u.Email == adminEmail);

        if (adminUser == null)
        {
            adminUser = new User
            {
                Email = adminEmail,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                Role = "Admin"
            };

            context.Users.Add(adminUser);
            context.SaveChanges(); // Guarda para obtener el Id
        }

        // Seed de tareas si no existen
        if (!context.Tasks.Any())
        {
            var tasks = new List<DailyTask>
            {
                new()
                {
                    Title = "Terminar reporte",
                    IsCompleted = false,
                    CreatedAt = DateTime.UtcNow.AddDays(-2),
                    UserId = adminUser.Id
                },
                new()
                {
                    Title = "Revisar correos",
                    IsCompleted = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-1),
                    UserId = adminUser.Id
                },
                new()
                {
                    Title = "Preparar reunión",
                    IsCompleted = false,
                    CreatedAt = DateTime.UtcNow,
                    UserId = adminUser.Id
                }
            };

            context.Tasks.AddRange(tasks);
            context.SaveChanges();
        }
    }
}

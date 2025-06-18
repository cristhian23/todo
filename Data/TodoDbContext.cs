using Microsoft.EntityFrameworkCore;

namespace TodoApi.Data;

public class TodoDbContext(DbContextOptions<TodoDbContext> options) : DbContext(options)
{
    public virtual DbSet<Models.DailyTask> Tasks { get; set; } = null!;
    public virtual DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.DailyTask>().ToTable("Tasks");
        modelBuilder.Entity<Models.DailyTask>().HasKey(t => t.Id);
        modelBuilder.Entity<Models.DailyTask>().Property(t => t.Title).IsRequired().HasMaxLength(200);
        modelBuilder.Entity<Models.DailyTask>().Property(t => t.IsCompleted).IsRequired();
        
    }
}
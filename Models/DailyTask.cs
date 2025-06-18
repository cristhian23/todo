namespace TodoApi.Models;
public class DailyTask
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public required string Title { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
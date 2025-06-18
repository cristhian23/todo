using TodoApi.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public IEnumerable<DailyTask> Tasks { get; set; }
    public string Role { get; set; } = "Admin";
}
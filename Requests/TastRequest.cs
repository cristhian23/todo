namespace TodoApi.Requests;

public class TaskRequest
{
    public int UserId { get; set; }
    public required string Title { get; set; }
    public bool IsCompleted { get; set; }
}
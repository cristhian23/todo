namespace   TodoApi.Services;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Interfaces;
using TodoApi.Requests;
using TaskModel = TodoApi.Models.DailyTask;

public class TaskService(TodoDbContext context) : ITaskService
{
	private readonly TodoDbContext _context = context;

    public async Task<IEnumerable<TaskModel>> GetAllTasksAsync()
	{
		var tasks = await _context.Tasks.AsNoTracking().ToListAsync();
		return tasks;
	}

	public async Task<IEnumerable<User>> GetUserByIdAsync(int userId, bool withTasks = false)
	{
		IQueryable<User> query = _context.Users;
		if (withTasks)
		{
			query = query.Include(u => u.Tasks);
		}
		var user = _context.Users.Where(u => u.Id == userId).AsNoTracking();
		return await query.ToListAsync();
	}

	public async Task<int> CreateTaskAsync(TaskRequest taskRequest)
	{
		var task = new TaskModel
		{
			Title = taskRequest.Title,
			IsCompleted = taskRequest.IsCompleted,
			CreatedAt = DateTime.UtcNow,
			UserId = taskRequest.UserId
		};
		_context.Tasks.Add(task);
		return await _context.SaveChangesAsync();
	}

	public async Task<int> UpdateTaskAsync(int taskId, TaskRequest taskRequest)
	{
		var task = await _context.Tasks.FindAsync(taskId);
		if (task == null)
		{
			return 0; // Task not found
		}
		task.Title = taskRequest.Title;
		task.IsCompleted = taskRequest.IsCompleted;
		return await _context.SaveChangesAsync();
	}

	public async Task<bool> DeleteTaskAsync(int taskId)
	{
		var task = await _context.Tasks.FindAsync(taskId);
		if (task == null)
		{
			return false; // Task not found
		}
		_context.Tasks.Remove(task);
		await _context.SaveChangesAsync();
		return true;
	}
}
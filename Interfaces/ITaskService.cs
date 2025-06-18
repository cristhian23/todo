
using TodoApi.Requests;
using TaskModel = TodoApi.Models.DailyTask;

namespace TodoApi.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskModel>> GetAllTasksAsync();
    Task<IEnumerable<User>> GetUserByIdAsync(int userId, bool withTasks = false);
    Task<int> CreateTaskAsync(TaskRequest request);
    Task<int> UpdateTaskAsync(int id, TaskRequest request);
    Task<bool> DeleteTaskAsync(int id);
}
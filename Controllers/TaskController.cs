namespace TodoApi.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Interfaces;
using TodoApi.Requests;
using TaskModel = Models.DailyTask;

[ApiController]
[Route("api/[controller]")]
public class TaskController(ITaskService taskService) : ControllerBase
{
    private readonly ITaskService _taskService = taskService;

    //[Authorize(Roles = "Admin")]
    //[Consumes("application/json")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskModel>>> GetAllTasks()
    {
        var tasks = await _taskService.GetAllTasksAsync();
        return Ok(tasks);
    }

    [Authorize(Policy = "All")]
    [Consumes("application/json")]
    [HttpGet]
    [Route("user/{userId}")]
    public async Task<ActionResult<IEnumerable<TaskModel>>> GetTasksByUserId(int userId, [FromQuery] bool withTasks = false)
    {
        var tasks = await _taskService.GetUserByIdAsync(userId, true);
        if (tasks == null || !tasks.Any())
            return NotFound();
        return Ok(tasks);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<int>> CreateTask([FromBody] TaskRequest taskRequest)
    {
        var taskId = await _taskService.CreateTaskAsync(taskRequest);
        return CreatedAtAction(nameof(GetAllTasks), new { id = taskId }, taskId);
    }


    [Authorize]
    [HttpPut("{taskId}")]
    public async Task<IActionResult> UpdateTask(int taskId, [FromBody] TaskRequest updateRequest)
    {
        var result = await _taskService.UpdateTaskAsync(taskId, updateRequest);
        if (result > 0)
            return Ok(result);
        return NotFound();
    }

    [Authorize]
    [HttpDelete("{taskId}")]
    public async Task<IActionResult> DeleteTask(int taskId)
    {
        var result = await _taskService.DeleteTaskAsync(taskId);
        if (result)
            return Ok(result);
        return NotFound();
    }
}
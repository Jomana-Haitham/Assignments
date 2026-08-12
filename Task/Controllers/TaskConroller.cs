using Microsoft.AspNetCore.Mvc;
using Task.DTOs;
using Task.Services;

namespace Task.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly TaskService _taskService;

    public TasksController(TaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetAll()
    {
        var tasks = await _taskService.GetAllAsync();

        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItemDto>> GetById(int id)
    {
        var task = await _taskService.GetByIdAsync(id);

        if (task == null)
            return NotFound();

        return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<TaskItemDto>> Create(
        CreateTaskRequest request)
    {
        var task = await _taskService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = task.Id },
            task);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TaskItemDto>> Update(
        int id,
        UpdateTaskRequest request)
    {
        var task = await _taskService.UpdateAsync(id, request);

        if (task == null)
            return NotFound();

        return Ok(task);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _taskService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
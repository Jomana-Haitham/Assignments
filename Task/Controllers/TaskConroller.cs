using Microsoft.AspNetCore.Mvc;
using Task.Models;
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

    
/// <summary>
/// Retrieves all tasks.
/// </summary>
/// <param name="filter">
/// Filtering, sorting, and pagination parameters.
/// </param>
/// <response code="200">
/// Returns a paginated list of tasks.
/// </response>
[ProducesResponseType(typeof(PagedResult<TaskItem>), StatusCodes.Status200OK)]

    [HttpGet]
    public ActionResult<PagedResult<TaskItem>> GetAll([FromQuery] TaskFilterParams filter)
    {
        var result = _taskService.GetAll(filter);
        return Ok(result);
    }
}
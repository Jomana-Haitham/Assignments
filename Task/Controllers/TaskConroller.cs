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

    [HttpGet]
    public ActionResult<PagedResult<TaskItem>> GetAll([FromQuery] TaskFilterParams filter)
    {
        var result = _taskService.GetAll(filter);
        return Ok(result);
    }
}
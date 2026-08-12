using AutoMapper;
using Task.DTOs;
using Task.Models;
using Task.Repositories;

namespace Task.Services;

public class TaskService
{
    private readonly ITaskRepository _repository;
    private readonly IMapper _mapper;

    public TaskService(
        ITaskRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TaskItemDto>> GetAllAsync()
    {
        var tasks = await _repository.GetAllAsync();

        return _mapper.Map<IEnumerable<TaskItemDto>>(tasks);
    }

    public async Task<TaskItemDto?> GetByIdAsync(int id)
    {
        var task = await _repository.GetByIdAsync(id);

        if (task == null)
            return null;

        return _mapper.Map<TaskItemDto>(task);
    }

    public async Task<TaskItemDto> CreateAsync(CreateTaskRequest request)
    {
        var task = _mapper.Map<TaskItem>(request);

        var createdTask = await _repository.CreateAsync(task);

        return _mapper.Map<TaskItemDto>(createdTask);
    }

    public async Task<TaskItemDto?> UpdateAsync(
        int id,
        UpdateTaskRequest request)
    {
        var task = await _repository.GetByIdAsync(id);

        if (task == null)
            return null;

        _mapper.Map(request, task);

        task.Id = id;

        var updated = await _repository.UpdateAsync(task);

        if (!updated)
            return null;

        var updatedTask = await _repository.GetByIdAsync(id);

        return updatedTask == null
            ? null
            : _mapper.Map<TaskItemDto>(updatedTask);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
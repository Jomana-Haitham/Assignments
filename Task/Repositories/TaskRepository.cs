using Microsoft.EntityFrameworkCore;
using Task.Data;
using Task.Models;

namespace Task.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        return await _context.Tasks
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        return await _context.Tasks
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TaskItem> CreateAsync(TaskItem task)
    {
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();

        return task;
    }

    public async Task<bool> UpdateAsync(TaskItem task)
    {
        var existingTask = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == task.Id);

        if (existingTask == null)
            return false;

        existingTask.Title = task.Title;
        existingTask.IsCompleted = task.IsCompleted;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == id);

        if (task == null)
            return false;

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();

        return true;
    }
}
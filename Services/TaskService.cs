using Microsoft.EntityFrameworkCore;
using Task.Data;
using Task.Models;

namespace Task.Services;

public class TaskService
{
    private readonly AppDbContext _context;

    public TaskService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<TaskItem>> GetAll(TaskFilterParams filter)
    {
        var query = _context.Tasks.AsQueryable();

        
        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            query = query.Where(t =>
                EF.Functions.ILike(t.Title, $"%{filter.Search}%"));
        }

        if (filter.IsCompleted.HasValue)
        {
            query = query.Where(t =>
                t.IsCompleted == filter.IsCompleted.Value);
        }

      
        var sortFields = new Dictionary<string, Func<TaskItem, object>>
        {
            { "title", t => t.Title },
            { "createdAt", t => t.CreatedAt },
            { "isCompleted", t => t.IsCompleted }
        };

        if (!sortFields.TryGetValue(filter.SortBy ?? "", out var sortSelector))
        {
            sortSelector = t => t.CreatedAt;
        }

        query = filter.SortDescending
            ? query.OrderByDescending(sortSelector).AsQueryable()
            : query.OrderBy(sortSelector).AsQueryable();

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return new PagedResult<TaskItem>
        {
            Items = items,
            TotalCount = totalCount,
            Page = filter.Page,
            PageSize = filter.PageSize,
            TotalPages = (int)Math.Ceiling((double)totalCount / filter.PageSize),
            HasNextPage = filter.Page * filter.PageSize < totalCount,
            HasPreviousPage = filter.Page > 1
        };
    }

    public async Task<TaskItem?> GetById(int id)
    {
        return await _context.Tasks.FindAsync(id);
    }

    public async Task<TaskItem> Create(TaskItem task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<TaskItem?> Update(int id, TaskItem updatedTask)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task == null)
        {
            return null;
        }

        task.Title = updatedTask.Title;
        task.IsCompleted = updatedTask.IsCompleted;
        task.CreatedAt = updatedTask.CreatedAt;
        task.UserId = updatedTask.UserId;

        await _context.SaveChangesAsync();

        return task;
    }

    public async Task<bool> Delete(int id)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task == null)
        {
            return false;
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();

        return true;
    }
}
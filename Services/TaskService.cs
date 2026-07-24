using Task.Models;

namespace Task.Services;

public class TaskService
{
    private readonly List<TaskItem> _tasks = new()
    {
        new TaskItem
        {
            Id = 1,
            Title = "Meeting",
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow.AddDays(-2)
        },
        new TaskItem
        {
            Id = 2,
            Title = "Shopping",
            IsCompleted = true,
            CreatedAt = DateTime.UtcNow.AddDays(-1)
        },
        new TaskItem
        {
            Id = 3,
            Title = "Gym",
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        }
    };

    public PagedResult<TaskItem> GetAll(TaskFilterParams filter)
    {
        var query = _tasks.AsQueryable();

        // Search
        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            query = query.Where(t =>
                t.Title.Contains(filter.Search,
                StringComparison.OrdinalIgnoreCase));
        }

        // Filter
        if (filter.IsCompleted.HasValue)
        {
            query = query.Where(t =>
                t.IsCompleted == filter.IsCompleted.Value);
        }

        // Sorting
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

        var totalCount = query.Count();

        var items = query
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToList();

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
}
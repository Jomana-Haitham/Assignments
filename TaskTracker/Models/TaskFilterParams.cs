namespace TaskTracker.Models;

public class TaskFilterParams : PaginationParams
{
    public string? Search { get; set; }

    public bool? IsCompleted { get; set; }

    public string? SortBy { get; set; }

    public bool SortDescending { get; set; }
}

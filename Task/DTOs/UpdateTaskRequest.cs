namespace Task.DTOs;

public class UpdateTaskRequest
{
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}
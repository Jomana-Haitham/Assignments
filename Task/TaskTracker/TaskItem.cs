namespace TaskTracker
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Priority Priority { get; set; }
        public TaskStatus Status { get; set; }

        public TaskItem(int id, string title, Priority priority, TaskStatus status)
        {
            Id = id;
            Title = title;
            Priority = priority;
            Status = status;
        }
    }
}
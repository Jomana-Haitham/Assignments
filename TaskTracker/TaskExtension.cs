using System.Collections.Generic;
using System.Linq;

namespace TaskTracker
{
    public static class TaskExtensions
    {
        public static IEnumerable<TaskItem> FilterByStatus( this IEnumerable<TaskItem> tasks, TaskStatus status)
        {
            return tasks.Where(t => t.Status == status);
        }

        public static IEnumerable<TaskItem> SortByPriority( this IEnumerable<TaskItem> tasks)
        {
            return tasks.OrderByDescending(t => t.Priority);
        }
    }
}
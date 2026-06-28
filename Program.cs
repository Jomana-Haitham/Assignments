using System;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTracker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Repository<TaskItem> repository = new Repository<TaskItem>();

            repository.Add(new TaskItem(1, "Finish Assignment", Priority.High, TaskStatus.InProgress));
            repository.Add(new TaskItem(2, "Buy Groceries", Priority.Low, TaskStatus.Pending));
            repository.Add(new TaskItem(3, "Study C#", Priority.Critical, TaskStatus.Pending));
            repository.Add(new TaskItem(4, "Workout", Priority.Medium, TaskStatus.Completed));

            var importantTasks = repository.FindAll(
                t => t.Priority == Priority.High || t.Priority == Priority.Critical);

            Console.WriteLine("High or Critical Tasks:");

            foreach (var task in importantTasks)
            {
                Console.WriteLine(task.Title);
            }

            Console.WriteLine();

            var pendingTasks = repository
                .GetAll()
                .FilterByStatus(TaskStatus.Pending)
                .SortByPriority();

            Func<TaskItem, string> formatter = task =>
                $"[{task.Priority}] {task.Title} - {task.Status}";

            Console.WriteLine("Pending Tasks:");

            foreach (var line in pendingTasks.Select(formatter))
            {
                Console.WriteLine(line);
            }

            Console.WriteLine();

            await SaveTasksAsync(repository.GetAll());

            Console.WriteLine("Finished.");
        }

        static async Task SaveTasksAsync(System.Collections.Generic.List<TaskItem> tasks)
        {
            Console.WriteLine("Saving tasks...");

            await Task.Delay(2000);

            Console.WriteLine($"{tasks.Count} tasks saved successfully.");
        }
    }
}

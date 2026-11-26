using TaskMaster.Session04.Data;
using TaskMaster.Session04.Models;

namespace TaskMaster.Session04.Demos;

/// <summary>Slides 21-22: a LINQ query describes WHAT to get; enumerating it GETS it.</summary>
public static class DeferredExecutionDemo
{
    public static void Run()
    {
        var tasks = SampleData.Create();

        // Nothing is filtered yet: 'query' is only a description.
        IEnumerable<TaskItem> query = tasks.Where(x => x.Priority == TaskPriority.High);

        // ToList() runs the query NOW and stores the results in a new list.
        List<TaskItem> snapshot = query.ToList();

        Console.WriteLine($"First run : query finds {query.Count()} high-priority tasks, snapshot has {snapshot.Count}");

        // Add one more high-priority task to the original list
        tasks.Add(new TaskItem
        {
            Id = 11,
            Title = "Brand-new urgent task",
            Status = TaskItemStatus.Pending,
            Priority = TaskPriority.High,
            DueDate = DateTime.UtcNow.AddDays(1)
        });

        Console.WriteLine($"After Add : query finds {query.Count()} (it ran again), snapshot still has {snapshot.Count} (stored earlier)");

        Console.WriteLine();
        Console.WriteLine("Changing a list while looping over it is a classic mistake:");
        var list = SampleData.Create();
        try
        {
            foreach (var task in list)
            {
                if (task.Status == TaskItemStatus.Completed)
                {
                    list.Remove(task);       // changes the list during the foreach
                }
            }
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"  InvalidOperationException: {ex.Message}");
            Console.WriteLine("  Fix: loop over list.ToList(), or use list.RemoveAll(x => ...).");
        }
    }
}

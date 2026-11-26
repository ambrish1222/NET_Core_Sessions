using TaskMaster.Session04.Data;
using TaskMaster.Session04.Models;

namespace TaskMaster.Session04.Exercises;

/// <summary>Exercise 1 (slide 28): eight queries over ten tasks.</summary>
public static class TaskQueriesExercise
{
    public static void Run()
    {
        List<TaskItem> tasks = SampleData.Create();      // 10 tasks
        Console.WriteLine($"Working with {tasks.Count} tasks");

        Show("1. Completed tasks", tasks.Where(x => x.Status == TaskItemStatus.Completed));

        Show("2. High-priority tasks", tasks.Where(x => x.Priority == TaskPriority.High));

        Show("3. High-priority and not completed",
            tasks.Where(x => x.Priority == TaskPriority.High && x.Status != TaskItemStatus.Completed));

        Show("4. Sorted by DueDate", tasks.OrderBy(x => x.DueDate));

        Console.WriteLine();
        Console.WriteLine("5. Titles only:");
        Console.WriteLine("   " + string.Join(" | ", tasks.Select(x => x.Title)));

        var task = tasks.FirstOrDefault(x => x.Id == 4);
        Console.WriteLine();
        Console.WriteLine($"6. Find by Id (4): {task?.Title ?? "not found"}");

        bool anyOverdue = tasks.Any(x => x.DueDate < DateTime.UtcNow && x.Status != TaskItemStatus.Completed);
        Console.WriteLine();
        Console.WriteLine($"7. Is any task overdue (past due and not completed)? {anyOverdue}");

        Console.WriteLine();
        Console.WriteLine($"8. Count: {tasks.Count()} total, {tasks.Count(x => x.Status == TaskItemStatus.Completed)} completed");
    }

    private static void Show(string heading, IEnumerable<TaskItem> items)
    {
        Console.WriteLine();
        Console.WriteLine(heading);
        foreach (var item in items)
        {
            Console.WriteLine($"   {item}");
        }
    }
}

using TaskMaster.Session04.Data;
using TaskMaster.Session04.Models;

namespace TaskMaster.Session04.Demos;

/// <summary>Slides 11-22: Where, Select, OrderBy, FirstOrDefault, Any, Count, ToList.</summary>
public static class LinqDemo
{
    // Slides 12-13
    public static void WhereFilter()
    {
        var tasks = SampleData.Create();

        Console.WriteLine("High priority tasks:");
        var highPriorityTasks = tasks.Where(x => x.Priority == TaskPriority.High);
        foreach (var task in highPriorityTasks)
        {
            Console.WriteLine($"  {task.Title}");
        }

        Console.WriteLine();
        Console.WriteLine("Urgent: High priority AND not completed:");
        var urgentTasks = tasks.Where(x =>
            x.Priority == TaskPriority.High &&
            x.Status != TaskItemStatus.Completed);
        foreach (var task in urgentTasks)
        {
            Console.WriteLine($"  {task.Title} ({task.Status})");
        }
    }

    // Slides 14-15: Where answers "which items?", Select answers "which data?"
    public static void SelectProject()
    {
        var tasks = SampleData.Create();

        Console.WriteLine("Select: only the titles");
        var titles = tasks.Select(x => x.Title);
        foreach (var title in titles)
        {
            Console.WriteLine($"  {title}");
        }

        Console.WriteLine();
        Console.WriteLine("Where + Select together: titles of completed tasks");
        var completedTitles = tasks
            .Where(x => x.Status == TaskItemStatus.Completed)
            .Select(x => x.Title);
        Console.WriteLine("  " + string.Join(", ", completedTitles));

        Console.WriteLine();
        Console.WriteLine("Select can build a new shape (an anonymous type):");
        foreach (var item in tasks.Take(3).Select(x => new { x.Id, x.Title }))
        {
            Console.WriteLine($"  {item.Id}: {item.Title}");
        }
    }

    // Slide 16
    public static void OrderBySort()
    {
        var tasks = SampleData.Create();

        Console.WriteLine("OrderBy DueDate (earliest first):");
        foreach (var task in tasks.OrderBy(x => x.DueDate).Take(4))
        {
            Console.WriteLine($"  {task.DueDate:yyyy-MM-dd}  {task.Title}");
        }

        Console.WriteLine();
        Console.WriteLine("OrderByDescending Priority, then by DueDate:");
        foreach (var task in tasks.OrderByDescending(x => x.Priority).ThenBy(x => x.DueDate).Take(4))
        {
            Console.WriteLine($"  {task.Priority,-6} {task.DueDate:yyyy-MM-dd}  {task.Title}");
        }
    }

    // Slides 17-19
    public static void FindAnyCount()
    {
        var tasks = SampleData.Create();

        // FirstOrDefault: first match, or null when nothing matches
        var found = tasks.FirstOrDefault(x => x.Id == 3);
        Console.WriteLine($"FirstOrDefault(Id == 3)  -> {found?.Title ?? "(null)"}");

        var task = tasks.FirstOrDefault(x => x.Id == 10_000);
        if (task is null)
        {
            Console.WriteLine("FirstOrDefault(Id == 10000) -> null, so: Task not found");
        }

        // Any: yes/no, stops at the first match
        bool hasHighPriority = tasks.Any(x => x.Priority == TaskPriority.High);
        Console.WriteLine($"Any high priority task?  -> {hasHighPriority}");

        // Count: everything, or only matching items
        int total = tasks.Count();
        int completed = tasks.Count(x => x.Status == TaskItemStatus.Completed);
        Console.WriteLine($"Count()                  -> {total}");
        Console.WriteLine($"Count(Completed)         -> {completed}");

        Console.WriteLine();
        Console.WriteLine("Prefer Any() over Count() > 0: Any stops at the first match.");
    }

    // Slide 3 of Session 5 uses this pattern: chain several operators together
    public static void Chaining()
    {
        var result = SampleData.Create()
            .Where(x => x.Priority == TaskPriority.High)
            .OrderBy(x => x.DueDate)
            .ToList();                         // ToList materializes the result

        Console.WriteLine($"Where + OrderBy + ToList -> {result.Count} tasks:");
        foreach (var task in result)
        {
            Console.WriteLine($"  {task}");
        }
    }
}

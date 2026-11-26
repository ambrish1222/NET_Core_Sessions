using TaskMaster.Session03.Domain;

namespace TaskMaster.Session03.Demos;

/// <summary>Slides 21-22: the complete TaskItem, with an optional Description.</summary>
public static class TaskModelDemo
{
    public static void Run()
    {
        Console.WriteLine("TaskItem");
        Console.WriteLine(" |- Id           (int)");
        Console.WriteLine(" |- Title        (string)");
        Console.WriteLine(" |- Description  (string?  <- may be null)");
        Console.WriteLine(" |- Status       (TaskItemStatus enum)");
        Console.WriteLine(" |- Priority     (TaskPriority enum)");
        Console.WriteLine(" `- DueDate      (DateTime)");
        Console.WriteLine();

        var task = new TaskItem
        {
            Id = 1,
            Title = "Learn C#",
            Status = TaskItemStatus.Pending,
            Priority = TaskPriority.High,
            DueDate = DateTime.UtcNow.AddDays(7)
        };

        Console.WriteLine(task);
        Console.WriteLine($"Description: {task.Description ?? "(none)"}");
    }
}

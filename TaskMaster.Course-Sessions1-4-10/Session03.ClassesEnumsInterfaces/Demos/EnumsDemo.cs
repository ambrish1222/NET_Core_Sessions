using TaskMaster.Session03.Domain;

namespace TaskMaster.Session03.Demos;

/// <summary>Slides 13-17: enums for fixed sets of values.</summary>
public static class EnumsDemo
{
    public static void Run()
    {
        // Slides 14-15: using an enum
        TaskItemStatus status = TaskItemStatus.Pending;
        if (status == TaskItemStatus.Completed)
        {
            Console.WriteLine("Task is complete");
        }
        else
        {
            Console.WriteLine("Task is not complete");
        }

        // Slide 16: priority enum
        TaskPriority priority = TaskPriority.High;
        Console.WriteLine($"Priority: {priority} (underlying number {(int)priority})");

        Console.WriteLine();
        Console.WriteLine("All allowed statuses: " + string.Join(", ", Enum.GetNames<TaskItemStatus>()));
        Console.WriteLine("All allowed priorities: " + string.Join(", ", Enum.GetNames<TaskPriority>()));

        // Slide 17: why not just use strings?
        Console.WriteLine();
        string statusText = "Complete";   // a typo: the compiler cannot warn us about strings
        Console.WriteLine($"string \"{statusText}\" == \"Completed\" ? {statusText == "Completed"}   (silent bug)");
        // TaskItemStatus bad = TaskItemStatus.Complete;   // COMPILE ERROR: the enum protects us

        // Converting text from outside (for example JSON or a query string) into an enum
        if (Enum.TryParse<TaskItemStatus>("completed", ignoreCase: true, out var parsed))
        {
            Console.WriteLine($"Enum.TryParse(\"completed\") -> {parsed}");
        }
        if (!Enum.TryParse<TaskItemStatus>("Done", ignoreCase: true, out _))
        {
            Console.WriteLine("Enum.TryParse(\"Done\")      -> not a valid status");
        }

        // switch works nicely with enums
        foreach (var value in Enum.GetValues<TaskItemStatus>())
        {
            string message = value switch
            {
                TaskItemStatus.Pending => "waiting to start",
                TaskItemStatus.InProgress => "being worked on",
                TaskItemStatus.Completed => "done",
                _ => "unknown"
            };
            Console.WriteLine($"  {value,-10} -> {message}");
        }
    }
}

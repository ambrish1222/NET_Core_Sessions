namespace TaskMaster.Session03.Demos;

/// <summary>
/// Slides 3-12: classes, objects, object initializers, properties and constructors.
/// The deck grows TaskItem step by step; here each step is its own small class
/// (TaskV1, TaskV2, TaskV3) so every version can run side by side.
/// </summary>
public static class ClassesDemo
{
    // Slide 3: recap of a method from Session 2
    public static void Recap()
    {
        string? title = "Learn C#";
        Console.WriteLine($"IsValidTitle(\"{title}\") = {IsValidTitle(title)}");
        Console.WriteLine("Today we move this kind of logic into classes.");
    }

    // Slides 4-7, 12
    public static void ClassAndObject()
    {
        // Slide 6: class = definition/type, object = instance
        TaskV1 task = new TaskV1();
        task.Id = 1;
        task.Title = "Learn C#";
        Console.WriteLine($"Created with new + assignments: {task.Id} {task.Title}");

        // Slide 7: object initializer
        TaskV1 task2 = new TaskV1
        {
            Id = 2,
            Title = "Learn ASP.NET Core"
        };
        Console.WriteLine($"Created with an object initializer: {task2.Id} {task2.Title}");

        // Slide 12: one class, many objects
        Console.WriteLine($"Same object? {ReferenceEquals(task, task2)}  (two objects built from one class)");
    }

    // Slides 8-9
    public static void Properties()
    {
        var task = new TaskV2 { Id = 1, Title = "Learn C#" };
        Console.WriteLine(task.Summary);

        task.IsCompleted = true;          // set
        Console.WriteLine(task.Summary);  // get (Summary is computed from other properties)

        Console.WriteLine();
        Console.WriteLine("get = read the value, set = assign/change it. A property can be read-only: { get; }");
    }

    // Slides 10-11
    public static void Constructors()
    {
        TaskV3 task = new TaskV3("Learn C#");
        Console.WriteLine($"Constructor created a valid task: '{task.Title}'");

        try
        {
            _ = new TaskV3("   ");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"The constructor refused an invalid task: {ex.Message}");
        }
    }

    private static bool IsValidTitle(string? title)
    {
        return !string.IsNullOrWhiteSpace(title);
    }

    // Slide 5
    private class TaskV1
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
    }

    // Slide 9
    private class TaskV2
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }

        public string Summary => $"{(IsCompleted ? "[x]" : "[ ]")} {Title}";
    }

    // Slide 11
    private class TaskV3
    {
        public string Title { get; }

        public TaskV3(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title is required.", nameof(title));
            }

            Title = title;
        }
    }
}

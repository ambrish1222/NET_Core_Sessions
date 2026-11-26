namespace TaskMaster.Session01.Demos;

/// <summary>Slides 14-18: DateTime, Guid and our first Task made of 6 variables.</summary>
public static class TaskDemo
{
    // Slide 14
    public static void DateTimeAndGuid()
    {
        DateTime dueDate = DateTime.UtcNow.AddDays(7);   // a deadline 7 days from now
        Guid taskId = Guid.NewGuid();                    // a globally unique id

        Console.WriteLine($"Due Date: {dueDate}");
        Console.WriteLine($"Task ID:  {taskId}");
        Console.WriteLine();
        Console.WriteLine($"UTC now  : {DateTime.UtcNow}   <- use this in backends");
        Console.WriteLine($"Local now: {DateTime.Now}   <- depends on the server's time zone");
        Console.WriteLine($"Another Guid: {Guid.NewGuid()}   <- different every time");
    }

    // Slides 16-18
    public static void TaskVariables()
    {
        string title = "Learn C#";
        string description = "Learn C# fundamentals";
        int priority = 1;                                // 1 = High, 2 = Medium, 3 = Low
        bool isCompleted = false;
        DateTime dueDate = DateTime.UtcNow.AddDays(7);
        Guid id = Guid.NewGuid();

        Console.WriteLine($"Task:        {title}");
        Console.WriteLine($"Description: {description}");
        Console.WriteLine($"Priority:    {priority}");
        Console.WriteLine($"Completed:   {isCompleted}");
        Console.WriteLine($"Due Date:    {dueDate}");
        Console.WriteLine($"Id:          {id}");
    }
}

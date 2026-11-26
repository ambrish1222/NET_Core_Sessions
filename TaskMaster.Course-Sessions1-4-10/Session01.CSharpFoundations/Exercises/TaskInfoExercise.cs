namespace TaskMaster.Session01.Exercises;

/// <summary>Exercise 2 (slide 20) and Homework task 2: Task information and reflection.</summary>
public static class TaskInfoExercise
{
    public static void Run()
    {
        Guid taskId = Guid.NewGuid();
        string title = "Learn C#";
        string description = "Learn C# fundamentals";
        int priority = 1;
        bool isCompleted = false;
        DateTime dueDate = DateTime.UtcNow.AddDays(7);

        // A local function prints the CURRENT values of the variables above.
        void PrintTask()
        {
            Console.WriteLine($"  Id:          {taskId}");
            Console.WriteLine($"  Title:       {title}");
            Console.WriteLine($"  Description: {description}");
            Console.WriteLine($"  Priority:    {priority}");
            Console.WriteLine($"  Completed:   {isCompleted}");
            Console.WriteLine($"  Due Date:    {dueDate}");
        }

        Console.WriteLine("Before:");
        PrintTask();

        // Step 4: change some values and print again
        isCompleted = true;
        title = "Learn C# (finished!)";

        Console.WriteLine();
        Console.WriteLine("After changing Completed and Title:");
        PrintTask();

        // Step 5: partner question
        Console.WriteLine();
        Console.WriteLine("Why were these types chosen?");
        Console.WriteLine("  Title, Description -> string   : words can be any length");
        Console.WriteLine("  Priority           -> int      : easy to compare and sort (1 < 2 < 3)");
        Console.WriteLine("  Completed          -> bool     : a task is either done or not done");
        Console.WriteLine("  Due Date           -> DateTime : supports date math like AddDays(7)");
        Console.WriteLine("  Id                 -> Guid     : practically zero id conflicts");
    }
}

namespace TaskMaster.Session02.Exercises;

/// <summary>Exercise 2 (slide 23): task status messages and a warning for urgent pending tasks.</summary>
public static class TaskStatusExercise
{
    public static void Run()
    {
        bool isCompleted = true;
        Console.WriteLine($"isCompleted = {isCompleted}");
        PrintStatus(isCompleted, priority: 1);

        isCompleted = false;
        Console.WriteLine($"isCompleted = {isCompleted}");
        PrintStatus(isCompleted, priority: 2);

        // The extension: a high-priority task that is still pending prints a warning.
        Console.WriteLine("isCompleted = False, priority = 1");
        PrintStatus(isCompleted, priority: 1);
    }

    private static void PrintStatus(bool isCompleted, int priority)
    {
        if (isCompleted)
        {
            Console.WriteLine("  Task completed.");
        }
        else
        {
            Console.WriteLine("  Task is still pending.");
            if (priority == 1)
            {
                Console.WriteLine("  WARNING: this is a HIGH-priority task and it is not finished!");
            }
        }
    }
}

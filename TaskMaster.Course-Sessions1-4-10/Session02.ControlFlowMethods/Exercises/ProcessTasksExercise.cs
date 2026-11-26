namespace TaskMaster.Session02.Exercises;

/// <summary>Exercise 3 (slide 24): process a List&lt;string&gt; with foreach and for.</summary>
public static class ProcessTasksExercise
{
    public static void Run()
    {
        var tasks = new List<string>
        {
            "Learn C#",
            "Learn .NET",
            "Practice if/else",
            "Write methods",
            "Build the Task API"
        };

        Console.WriteLine("foreach: print each task");
        foreach (var task in tasks)
        {
            Console.WriteLine($"  {task}");
        }

        Console.WriteLine();
        Console.WriteLine("for: print the task number");
        for (int i = 0; i < tasks.Count; i++)
        {
            Console.WriteLine($"  Task {i + 1}: {tasks[i]}");
        }

        Console.WriteLine();
        Console.WriteLine("When is foreach preferable?");
        Console.WriteLine("  When you only need each item and not its position: it is shorter and");
        Console.WriteLine("  you cannot get the index wrong. Use for when you need the index, a");
        Console.WriteLine("  fixed number of repetitions, or to go backwards / skip items.");
    }
}

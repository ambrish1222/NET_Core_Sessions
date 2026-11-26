namespace TaskMaster.Session02.Demos;

/// <summary>Slides 9-10: for and foreach.</summary>
public static class LoopsDemo
{
    // Slide 9: use for when you need an index or a known number of repetitions
    public static void ForLoop()
    {
        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine($"Task {i}");
        }
    }

    // Slide 10: use foreach to process every item in a collection
    public static void ForeachLoop()
    {
        var tasks = new List<string>
        {
            "Learn C#",
            "Learn .NET",
            "Build API"
        };

        foreach (var task in tasks)
        {
            Console.WriteLine(task);
        }

        Console.WriteLine();
        Console.WriteLine("for gives you the index, foreach does not:");
        for (int i = 0; i < tasks.Count; i++)
        {
            Console.WriteLine($"  #{i + 1}: {tasks[i]}");
        }
    }
}

namespace TaskMaster.Session02.Demos;

/// <summary>Slides 11-15: methods, parameters, return values and a validation method.</summary>
public static class MethodsDemo
{
    public static void Run()
    {
        // Slide 12: no parameters
        PrintWelcome();

        // Slide 13: with parameters
        PrintTask("Learn C#", 1);

        // Slide 14: with a return value
        bool result = IsHighPriority(1);
        Console.WriteLine($"IsHighPriority(1) = {result}");
        Console.WriteLine($"IsHighPriority(3) = {IsHighPriority(3)}");

        // Slide 15: validation method
        string title = "Learn C#";
        if (IsValidTitle(title))
        {
            Console.WriteLine("Valid title");
        }
    }

    private static void PrintWelcome()
    {
        Console.WriteLine("Welcome to Task API");
    }

    private static void PrintTask(string title, int priority)
    {
        Console.WriteLine($"Task: {title}, Priority: {priority}");
    }

    private static bool IsHighPriority(int priority)
    {
        return priority == 1;
    }

    private static bool IsValidTitle(string title)
    {
        return !string.IsNullOrWhiteSpace(title);
    }
}

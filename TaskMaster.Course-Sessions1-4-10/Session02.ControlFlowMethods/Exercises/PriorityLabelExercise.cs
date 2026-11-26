namespace TaskMaster.Session02.Exercises;

/// <summary>Exercise 1 (slide 22): GetPriorityLabel with a switch expression.</summary>
public static class PriorityLabelExercise
{
    public static void Run()
    {
        foreach (int priority in new[] { 1, 2, 3, 0, 4, -5 })
        {
            Console.WriteLine($"GetPriorityLabel({priority,2}) = {GetPriorityLabel(priority)}");
        }
    }

    // In this session 1 = High, 2 = Medium, 3 = Low (as in the deck).
    public static string GetPriorityLabel(int priority)
    {
        return priority switch
        {
            1 => "High",
            2 => "Medium",
            3 => "Low",
            _ => "Unknown"
        };
    }
}

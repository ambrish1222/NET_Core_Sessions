namespace TaskMaster.Session02.Exercises;

/// <summary>Exercise 4 (slide 25): IsValidTitle(string?) tested with null, empty, whitespace and a real title.</summary>
public static class ValidationExercise
{
    public static void Run()
    {
        string?[] titles = { null, "", "   ", "Learn C#" };

        foreach (var title in titles)
        {
            string shown = title is null ? "null" : $"\"{title}\"";
            Console.WriteLine($"IsValidTitle({shown,-10}) = {IsValidTitle(title)}");
        }
    }

    public static bool IsValidTitle(string? title)
    {
        return !string.IsNullOrWhiteSpace(title);
    }
}

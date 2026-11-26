namespace TaskMaster.Session02.Demos;

/// <summary>Slides 16-21: null, nullable reference types, ?? and ?.</summary>
public static class NullDemo
{
    // Slides 16-17
    public static void NullableReference()
    {
        string title = "Learn C#";        // must never be null
        string? description = null;       // the ? says: null is allowed here

        Console.WriteLine($"title       = '{title}'");
        Console.WriteLine($"description = '{description}'   (null prints as nothing)");
        Console.WriteLine($"description is null? {description is null}");
        // Uncomment the next line: the compiler warns because 'description' may be null.
        // Console.WriteLine(description.Length);
    }

    // Slides 18-19
    public static void NullOperators()
    {
        string? description = null;

        // ?? = "use the right side when the left side is null"
        string display = description ?? "No description";
        Console.WriteLine($"description ?? \"No description\" -> {display}");

        // ?. = "only access the member when the value is not null"
        int? lengthOrNull = description?.Length;
        int length = description?.Length ?? 0;
        Console.WriteLine($"description?.Length          -> {(lengthOrNull is null ? "null" : lengthOrNull.ToString())}");
        Console.WriteLine($"description?.Length ?? 0     -> {length}");

        description = "Learn nullable types";
        Console.WriteLine($"With a value: ?? -> '{description ?? "No description"}', ?. -> {description?.Length}");
    }

    // Slide 20
    public static void EmptyVsNull()
    {
        string?[] samples = { null, "", "   ", "Learn C#" };

        Console.WriteLine($"{"value",-12} {"IsNullOrEmpty",-14} {"IsNullOrWhiteSpace",-20}");
        foreach (var s in samples)
        {
            string shown = s is null ? "null" : $"\"{s}\"";
            Console.WriteLine($"{shown,-12} {string.IsNullOrEmpty(s),-14} {string.IsNullOrWhiteSpace(s),-20}");
        }
        Console.WriteLine();
        Console.WriteLine("For required text typed by a user, string.IsNullOrWhiteSpace is usually the right check.");
    }

    // Slide 21
    public static void TitleValidation()
    {
        string? title = "Learn C#";
        if (!IsValidTitle(title))
        {
            Console.WriteLine("Title is required");
        }
        else
        {
            Console.WriteLine("Title is valid");
        }

        title = null;
        if (!IsValidTitle(title))
        {
            Console.WriteLine("Title is required");
        }
    }

    private static bool IsValidTitle(string? title)
    {
        return !string.IsNullOrWhiteSpace(title);
    }
}

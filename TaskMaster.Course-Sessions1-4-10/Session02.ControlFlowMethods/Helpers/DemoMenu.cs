using System.Text;

namespace TaskMaster.Session02.Helpers;

/// <summary>One line in the menu: a key, a title, the slide numbers and the code to run.</summary>
public sealed record DemoEntry(string Key, string Title, string Slides, Action Run);

/// <summary>
/// Tiny menu runner so every demo and exercise can be started on its own.
///   dotnet run               -> interactive menu
///   dotnet run -- 5 E2       -> run item 5 and item E2, then exit
///   dotnet run -- all        -> run everything, then exit
/// </summary>
public static class DemoMenu
{
    public static void Run(string heading, IReadOnlyList<DemoEntry> entries, string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        if (args.Length > 0)
        {
            foreach (var arg in args)
            {
                RunByKey(entries, arg);
            }
            return;
        }

        while (true)
        {
            PrintMenu(heading, entries);
            Console.Write("Choose an item (or q to quit): ");
            var choice = Console.ReadLine()?.Trim();
            if (choice is null || choice.Equals("q", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            RunByKey(entries, choice);

            Console.WriteLine();
            Console.Write("Press Enter to go back to the menu...");
            if (Console.ReadLine() is null)
            {
                return;
            }
        }
    }

    private static void PrintMenu(string heading, IReadOnlyList<DemoEntry> entries)
    {
        Console.WriteLine();
        Console.WriteLine(new string('=', 72));
        Console.WriteLine(heading);
        Console.WriteLine(new string('=', 72));
        foreach (var entry in entries)
        {
            Console.WriteLine($"  {entry.Key,-4} {entry.Title}  (slides {entry.Slides})");
        }
        Console.WriteLine("  all  Run everything");
        Console.WriteLine("  q    Quit");
    }

    private static void RunByKey(IReadOnlyList<DemoEntry> entries, string key)
    {
        if (key.Equals("all", StringComparison.OrdinalIgnoreCase))
        {
            foreach (var entry in entries)
            {
                RunEntry(entry);
            }
            return;
        }

        var match = entries.FirstOrDefault(e => e.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
        if (match is null)
        {
            Console.WriteLine($"Unknown item '{key}'.");
            return;
        }

        RunEntry(match);
    }

    private static void RunEntry(DemoEntry entry)
    {
        Console.WriteLine();
        Console.WriteLine($"--- {entry.Key}: {entry.Title} (slides {entry.Slides}) ---");
        try
        {
            entry.Run();
        }
        catch (Exception ex)
        {
            // Menu boundary: keep the menu alive if one item fails.
            Console.WriteLine($"[This item failed] {ex.GetType().Name}: {ex.Message}");
        }
    }
}

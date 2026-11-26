namespace TaskMaster.Session02.Homework;

/// <summary>Runs the homework checklist: 5+ tasks with foreach, plus tests for null and invalid input.</summary>
public static class HomeworkDemo
{
    public static void Run()
    {
        Console.WriteLine("Title checks:");
        string?[] titles = { null, "", "   ", "Learn C#" };
        foreach (var title in titles)
        {
            string shown = title is null ? "null" : $"\"{title}\"";
            Console.WriteLine($"  IsValidTitle({shown,-10}) = {TaskValidator.IsValidTitle(title)}");
        }

        Console.WriteLine();
        Console.WriteLine("Priority checks:");
        foreach (int priority in new[] { 0, 1, 2, 3, 4 })
        {
            Console.WriteLine($"  IsValidPriority({priority}) = {TaskValidator.IsValidPriority(priority)}   label = {TaskValidator.GetPriorityLabel(priority)}");
        }

        Console.WriteLine();
        Console.WriteLine("Processing tasks with foreach:");
        var tasks = new List<(string? Title, int Priority, bool Completed)>
        {
            ("Learn C#", 1, false),
            ("Learn .NET", 2, true),
            ("Practice loops", 3, false),
            ("Write methods", 1, true),
            ("Build the Task API", 2, false),
            (null, 1, false),          // invalid title
            ("Bad priority task", 7, false)
        };

        foreach (var (title, priority, completed) in tasks)
        {
            if (!TaskValidator.IsValidTitle(title))
            {
                Console.WriteLine("  [skipped] title is required");
                continue;
            }

            Console.WriteLine($"  {title,-20} -> {TaskValidator.GetTaskMessage(completed, priority)}");
        }
    }
}

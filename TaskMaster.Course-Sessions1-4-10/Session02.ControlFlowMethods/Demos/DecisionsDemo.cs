namespace TaskMaster.Session02.Demos;

/// <summary>Slides 3-8: recap, operators, if/else and the switch expression.</summary>
public static class DecisionsDemo
{
    // Slide 3
    public static void Recap()
    {
        string title = "Learn C#";
        int priority = 1;
        bool completed = false;
        Console.WriteLine($"Task: {title}");
        Console.WriteLine($"Priority: {priority}");
        Console.WriteLine($"Completed: {completed}");
    }

    // Slides 4-5
    public static void Operators()
    {
        int priority = 1;
        bool completed = false;

        Console.WriteLine("Comparison operators always produce a bool:");
        Console.WriteLine($"  priority == 1 -> {priority == 1}");
        Console.WriteLine($"  priority != 1 -> {priority != 1}");
        Console.WriteLine($"  priority >  2 -> {priority > 2}");
        Console.WriteLine($"  priority <  2 -> {priority < 2}");
        Console.WriteLine($"  priority >= 1 -> {priority >= 1}");
        Console.WriteLine($"  priority <= 0 -> {priority <= 0}");

        Console.WriteLine();
        Console.WriteLine("Logical operators: && (AND), || (OR), ! (NOT)");
        bool isHighPriority = priority == 1;
        bool isIncomplete = completed == false;
        bool needsAttention = isHighPriority && isIncomplete;
        Console.WriteLine($"  isHighPriority = {isHighPriority}, isIncomplete = {isIncomplete}");
        Console.WriteLine($"  needsAttention (AND) = {needsAttention}");
        Console.WriteLine($"  either one is true (OR) = {isHighPriority || isIncomplete}");
        Console.WriteLine($"  NOT isHighPriority = {!isHighPriority}");
    }

    // Slides 6-7
    public static void IfElse()
    {
        int priority = 1;
        if (priority == 1)
        {
            Console.WriteLine("High priority");
        }
        else
        {
            Console.WriteLine("Normal priority");
        }

        Console.WriteLine();
        Console.WriteLine("Multiple conditions, tried with several tasks:");
        (int Priority, bool Completed)[] samples = { (1, false), (1, true), (2, false) };
        foreach (var (p, done) in samples)
        {
            Console.Write($"  priority={p}, completed={done,-5} -> ");
            if (p == 1 && !done)
            {
                Console.WriteLine("High-priority incomplete task");
            }
            else if (done)
            {
                Console.WriteLine("Task completed");
            }
            else
            {
                Console.WriteLine("Normal task");
            }
        }
    }

    // Slide 8
    public static void SwitchExpression()
    {
        foreach (int priority in new[] { 1, 2, 3, 99 })
        {
            string label = priority switch
            {
                1 => "High",
                2 => "Medium",
                3 => "Low",
                _ => "Unknown"      // _ is the "everything else" case
            };
            Console.WriteLine($"priority {priority,2} -> {label}");
        }
    }
}

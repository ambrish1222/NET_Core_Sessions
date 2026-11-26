using TaskMaster.Session04.Models;
using TaskMaster.Session04.Validation;

namespace TaskMaster.Session04.Demos;

/// <summary>Slides 23-27: exceptions, throw, try/catch and guard clauses.</summary>
public static class ExceptionsDemo
{
    // Slides 24-25
    public static void ThrowAndCatch()
    {
        try
        {
            TaskValidator.ValidatePriority(2);
            Console.WriteLine("ValidatePriority(2) -> fine, nothing thrown");

            TaskValidator.ValidatePriority(10);
            Console.WriteLine("This line is never reached.");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Invalid priority: {ex.Message}");
        }
    }

    // Slides 26-27
    public static void GuardClauses()
    {
        var samples = new List<(string Name, TaskItem? Task)>
        {
            ("valid task",      new TaskItem { Id = 1, Title = "Learn C#", Priority = TaskPriority.High }),
            ("null task",       null),
            ("whitespace title", new TaskItem { Id = 2, Title = "   ", Priority = TaskPriority.Low }),
            ("bad priority",    new TaskItem { Id = 3, Title = "Odd one", Priority = (TaskPriority)99 })
        };

        foreach (var (name, task) in samples)
        {
            try
            {
                TaskValidator.ValidateTask(task!);
                Console.WriteLine($"  {name,-17} -> valid");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"  {name,-17} -> ArgumentNullException (parameter '{ex.ParamName}')");
            }
            catch (ArgumentException ex)
            {
                // ArgumentException is the parent type, so it must come AFTER the more specific one.
                Console.WriteLine($"  {name,-17} -> ArgumentException: {ex.Message.Split('\n')[0]}");
            }
        }
    }

    // Slide 23: expected bad input should be checked, not caught
    public static void ExpectedVsUnexpected()
    {
        string userInput = "abc";

        Console.WriteLine("Expected problem (the user typed text instead of a number):");
        if (int.TryParse(userInput, out int priority))
        {
            Console.WriteLine($"  parsed {priority}");
        }
        else
        {
            Console.WriteLine("  int.TryParse returned false: handle it with an if, no exception needed.");
        }

        Console.WriteLine();
        Console.WriteLine("Do NOT do this:  catch (Exception) { }   <- hides every bug, even ones you never expected.");
        Console.WriteLine("Catch a specific exception only where you can handle or translate it,");
        Console.WriteLine("usually at a boundary such as the API layer (later: middleware).");
    }
}

using TaskMaster.Session04.Models;
using TaskMaster.Session04.Repositories;
using TaskMaster.Session04.Validation;

namespace TaskMaster.Session04.Exercises;

/// <summary>Exercise 3 (slide 30): ValidateTask before adding, caught at one suitable boundary.</summary>
public static class ExceptionHandlingExercise
{
    public static void Run()
    {
        ITaskRepository repository = new InMemoryTaskRepository();

        var attempts = new List<(string Name, TaskItem? Task)>
        {
            ("valid task",       new TaskItem { Id = 1, Title = "Learn exceptions", Priority = TaskPriority.High }),
            ("null task",        null),
            ("empty title",      new TaskItem { Id = 2, Title = "", Priority = TaskPriority.Low }),
            ("invalid priority", new TaskItem { Id = 3, Title = "Odd", Priority = (TaskPriority)42 }),
            ("duplicate id",     new TaskItem { Id = 1, Title = "Same id", Priority = TaskPriority.Low })
        };

        foreach (var (name, task) in attempts)
        {
            // The repository validates and throws; THIS loop is the boundary that catches and reports.
            try
            {
                repository.Add(task!);
                Console.WriteLine($"  {name,-17} -> added");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"  {name,-17} -> rejected ({ex.GetType().Name}): {ex.Message.Split('\n')[0]}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"  {name,-17} -> rejected ({ex.GetType().Name}): {ex.Message}");
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Tasks stored: {repository.GetAll().Count()} (only the valid one)");

        // Directly calling the validator, as the exercise describes:
        try
        {
            TaskValidator.ValidateTask(new TaskItem { Id = 9, Title = "  " });
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"ValidateTask directly: {ex.Message.Split('\n')[0]}");
        }
    }
}

using TaskMaster.Session04.Data;
using TaskMaster.Session04.Repositories;

namespace TaskMaster.Session04.Exercises;

/// <summary>Exercise 2 (slide 29): InMemoryTaskRepository with GetAll, GetById (FirstOrDefault) and Delete.</summary>
public static class RepositoryExercise
{
    public static void Run()
    {
        ITaskRepository repository = new InMemoryTaskRepository();
        foreach (var task in SampleData.Create())
        {
            repository.Add(task);
        }

        Console.WriteLine($"GetAll(): {repository.GetAll().Count()} tasks");
        Console.WriteLine($"GetById(4): {repository.GetById(4)?.Title ?? "not found"}");
        Console.WriteLine($"GetById(404): {repository.GetById(404)?.Title ?? "not found (null)"}");

        Console.WriteLine($"Delete(4): {repository.Delete(4)}   (true = deleted)");
        Console.WriteLine($"Delete(4) again: {repository.Delete(4)}   (false = nothing to delete)");

        try
        {
            repository.Delete(-1);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Delete(-1) -> exception, an id like that is a caller mistake: {ex.ParamName}");
        }

        Console.WriteLine();
        Console.WriteLine("Discussion: should the repository expose List<TaskItem> or IEnumerable<TaskItem>?");
        Console.WriteLine("  IEnumerable<TaskItem>: callers can read and loop, but cannot Add/Remove behind our back.");
        Console.WriteLine("  Return a missing item as null (GetById) or false (Delete): not finding something is normal,");
        Console.WriteLine("  while an invalid id like -1 is a contract violation and throws.");
    }
}

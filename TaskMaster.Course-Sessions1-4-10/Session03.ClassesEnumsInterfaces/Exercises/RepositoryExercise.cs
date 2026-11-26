using TaskMaster.Session03.Domain;

namespace TaskMaster.Session03.Exercises;

/// <summary>Exercise 2 (slide 26): use every operation of the repository.</summary>
public static class RepositoryExercise
{
    public static void Run()
    {
        ITaskRepository repository = new InMemoryTaskRepository();

        // Add
        foreach (var task in SampleData.Create().Take(3))
        {
            repository.Add(task);
        }
        Console.WriteLine($"After Add x3: {repository.GetAll().Count()} tasks");

        // GetById
        var found = repository.GetById(2);
        Console.WriteLine($"GetById(2): {found}");

        // Update: replace task 2 with a changed version
        if (found is not null)
        {
            found.Status = TaskItemStatus.Completed;
            repository.Update(found);
        }
        Console.WriteLine($"After Update: {repository.GetById(2)}");

        // Delete
        repository.Delete(1);
        Console.WriteLine($"After Delete(1): {repository.GetAll().Count()} tasks, GetById(1) -> {(repository.GetById(1) is null ? "null" : "found")}");

        // Homework: duplicate ids are rejected
        try
        {
            repository.Add(new TaskItem { Id = 2, Title = "Duplicate id" });
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Duplicate id rejected: {ex.Message}");
        }

        // Deleting something that does not exist
        try
        {
            repository.Delete(999);
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine($"Missing task rejected: {ex.Message}");
        }
    }
}

using TaskMaster.Session03.Domain;

namespace TaskMaster.Session03.Homework;

/// <summary>Slide 29 checklist: complete repository, duplicate-id validation, 5 sample tasks and the written flow.</summary>
public static class HomeworkDemo
{
    public static void Run()
    {
        ITaskRepository repository = new InMemoryTaskRepository();
        foreach (var task in SampleData.Create())
        {
            repository.Add(task);
        }
        Console.WriteLine($"[x] At least 5 sample tasks: {repository.GetAll().Count()} loaded");

        Console.WriteLine($"[x] GetById returns null when missing: {(repository.GetById(42) is null)}");

        try
        {
            repository.Add(new TaskItem { Id = 3, Title = "Same id again" });
            Console.WriteLine("[ ] Duplicate id was accepted (this is a bug)");
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("[x] Duplicate ids are rejected");
        }

        var task4 = repository.GetById(4)!;
        task4.Priority = TaskPriority.High;
        repository.Update(task4);
        repository.Delete(5);
        Console.WriteLine($"[x] Update and Delete work: {repository.GetAll().Count()} tasks left, task 4 is now {repository.GetById(4)!.Priority}");

        Console.WriteLine();
        Console.WriteLine("The flow to write down:");
        Console.WriteLine("  TaskItem  ->  ITaskRepository  ->  InMemoryTaskRepository");
        Console.WriteLine("  (the data)    (the contract)       (the implementation)");
    }
}

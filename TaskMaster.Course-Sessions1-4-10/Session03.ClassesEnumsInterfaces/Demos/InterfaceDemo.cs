using TaskMaster.Session03.Domain;

namespace TaskMaster.Session03.Demos;

/// <summary>Slides 18-20 and 23-24: interfaces, implementations and why they matter.</summary>
public static class InterfaceDemo
{
    public static void Run()
    {
        // The VARIABLE type is the interface. The OBJECT is the in-memory implementation.
        ITaskRepository repository = new InMemoryTaskRepository();
        foreach (var task in SampleData.Create())
        {
            repository.Add(task);
        }

        Console.WriteLine($"GetAll() returned {repository.GetAll().Count()} tasks");
        Console.WriteLine($"GetById(2)   -> {repository.GetById(2)?.ToString() ?? "not found"}");
        Console.WriteLine($"GetById(999) -> {repository.GetById(999)?.ToString() ?? "null (not found)"}");

        // The consumer only knows the contract...
        Console.WriteLine();
        Console.WriteLine("Using the in-memory implementation:");
        PrintPendingTasks(repository);

        // ...so we can swap the implementation without changing the consumer.
        Console.WriteLine();
        Console.WriteLine("Using a different implementation of the SAME interface:");
        ITaskRepository logged = new LoggingTaskRepository(repository);
        PrintPendingTasks(logged);

        Console.WriteLine();
        Console.WriteLine("Today -> InMemoryTaskRepository.  Week 3 -> EF Core + SQL Server.");
        Console.WriteLine("The code that depends on ITaskRepository does not need to change.");
    }

    // This method works with ANY implementation of ITaskRepository.
    private static void PrintPendingTasks(ITaskRepository repository)
    {
        foreach (var task in repository.GetAll())
        {
            if (task.Status == TaskItemStatus.Pending)
            {
                Console.WriteLine($"  {task}");
            }
        }
    }

    /// <summary>A second implementation: it wraps another repository and logs every call.</summary>
    private sealed class LoggingTaskRepository : ITaskRepository
    {
        private readonly ITaskRepository _inner;

        public LoggingTaskRepository(ITaskRepository inner)
        {
            _inner = inner;
        }

        public IEnumerable<TaskItem> GetAll()
        {
            Console.WriteLine("  [log] GetAll()");
            return _inner.GetAll();
        }

        public TaskItem? GetById(int id)
        {
            Console.WriteLine($"  [log] GetById({id})");
            return _inner.GetById(id);
        }

        public void Add(TaskItem task)
        {
            Console.WriteLine($"  [log] Add({task.Id})");
            _inner.Add(task);
        }

        public void Update(TaskItem task)
        {
            Console.WriteLine($"  [log] Update({task.Id})");
            _inner.Update(task);
        }

        public void Delete(int id)
        {
            Console.WriteLine($"  [log] Delete({id})");
            _inner.Delete(id);
        }
    }
}

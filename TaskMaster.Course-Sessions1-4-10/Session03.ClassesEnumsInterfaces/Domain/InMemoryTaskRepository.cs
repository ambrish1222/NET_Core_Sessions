namespace TaskMaster.Session03.Domain;

/// <summary>
/// Slide 20 + Exercise 2 + Homework: stores tasks in a List&lt;TaskItem&gt; in RAM.
/// Today: in memory. Week 3: EF Core + SQL Server behind the same interface.
/// </summary>
public class InMemoryTaskRepository : ITaskRepository
{
    private readonly List<TaskItem> _tasks = new();

    // Return a copy so callers cannot change our internal list by accident.
    public IEnumerable<TaskItem> GetAll()
    {
        return _tasks.ToList();
    }

    // Returns null when nothing matches (homework requirement).
    public TaskItem? GetById(int id)
    {
        return _tasks.FirstOrDefault(x => x.Id == id);
    }

    public void Add(TaskItem task)
    {
        ArgumentNullException.ThrowIfNull(task);

        // Homework: validate duplicate ids
        if (_tasks.Any(x => x.Id == task.Id))
        {
            throw new InvalidOperationException($"A task with Id {task.Id} already exists.");
        }

        _tasks.Add(task);
    }

    public void Update(TaskItem task)
    {
        ArgumentNullException.ThrowIfNull(task);

        var index = _tasks.FindIndex(x => x.Id == task.Id);
        if (index < 0)
        {
            throw new KeyNotFoundException($"Task {task.Id} was not found.");
        }

        _tasks[index] = task;
    }

    public void Delete(int id)
    {
        var existing = GetById(id);
        if (existing is null)
        {
            throw new KeyNotFoundException($"Task {id} was not found.");
        }

        _tasks.Remove(existing);
    }
}

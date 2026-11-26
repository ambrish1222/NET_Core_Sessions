using TaskMaster.Session04.Models;
using TaskMaster.Session04.Validation;

namespace TaskMaster.Session04.Repositories;

/// <summary>A List&lt;TaskItem&gt; behind the ITaskRepository contract, using LINQ for every query.</summary>
public class InMemoryTaskRepository : ITaskRepository
{
    private readonly List<TaskItem> _tasks = new();

    // Discussion (slide 29): expose IEnumerable<TaskItem>, not List<TaskItem>.
    // A List lets callers Add/Remove behind our back; IEnumerable only lets them read.
    // We also return a copy, so even a cast back to List cannot change our data.
    public IEnumerable<TaskItem> GetAll()
    {
        return _tasks.ToList();
    }

    public TaskItem? GetById(int id)
    {
        return _tasks.FirstOrDefault(x => x.Id == id);
    }

    public void Add(TaskItem task)
    {
        TaskValidator.ValidateTask(task);   // guard clauses first (Exercise 3)

        if (_tasks.Any(x => x.Id == task.Id))
        {
            throw new InvalidOperationException($"A task with Id {task.Id} already exists.");
        }

        _tasks.Add(task);
    }

    public bool Delete(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id), id, "Id must be greater than 0.");
        }

        var existing = _tasks.FirstOrDefault(x => x.Id == id);
        if (existing is null)
        {
            return false;   // expected situation, so return a result instead of throwing
        }

        _tasks.Remove(existing);
        return true;
    }

    public IEnumerable<TaskItem> GetByStatus(TaskItemStatus status)
    {
        return _tasks.Where(x => x.Status == status).ToList();
    }

    public IEnumerable<TaskItem> GetByPriority(TaskPriority priority)
    {
        return _tasks.Where(x => x.Priority == priority).ToList();
    }

    public IEnumerable<TaskItem> SearchByTitle(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return GetAll();
        }

        return _tasks
            .Where(x => x.Title.Contains(text, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public IEnumerable<TaskItem> GetSortedByDueDate(bool descending = false)
    {
        return descending
            ? _tasks.OrderByDescending(x => x.DueDate).ToList()
            : _tasks.OrderBy(x => x.DueDate).ToList();
    }
}

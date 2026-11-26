using TaskMaster.Api.Models;

namespace TaskMaster.Api.Repositories;

/// <summary>
/// Stores tasks in a List&lt;TaskItem&gt; in RAM. Data is lost when the app restarts.
/// Registered as a Singleton (see Program.cs) so the list survives between requests.
/// Kept in Session 10 so you can switch back with  "Persistence": { "Provider": "InMemory" }  (Exercise 3).
/// </summary>
public class InMemoryTaskRepository : ITaskRepository
{
    private readonly List<TaskItem> _tasks = new();
    private readonly object _lock = new();   // web requests run in parallel; protect the list
    private int _nextId = 1;

    public InMemoryTaskRepository()
    {
        // Seed data so GET /api/tasks returns something on the first run.
        _tasks.Add(new TaskItem { Id = _nextId++, Title = "Learn thin controllers", Priority = TaskPriority.High, Status = TaskItemStatus.InProgress, CreatedAt = DateTime.UtcNow });
        _tasks.Add(new TaskItem { Id = _nextId++, Title = "Write the service layer", Priority = TaskPriority.Medium, Status = TaskItemStatus.Pending, CreatedAt = DateTime.UtcNow });
    }

    public Task<IReadOnlyList<TaskItem>> GetAllAsync(CancellationToken cancellationToken)
    {
        // A cancelled request stops here instead of doing useless work.
        cancellationToken.ThrowIfCancellationRequested();

        lock (_lock)
        {
            IReadOnlyList<TaskItem> snapshot = _tasks.OrderBy(t => t.Id).ToList();
            return Task.FromResult(snapshot);
        }
    }

    public Task<TaskItem?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        lock (_lock)
        {
            return Task.FromResult(_tasks.FirstOrDefault(t => t.Id == id));
        }
    }

    public Task AddAsync(TaskItem task, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        lock (_lock)
        {
            task.Id = _nextId++;   // the repository (the "database") assigns the Id
            _tasks.Add(task);
        }
        return Task.CompletedTask;
    }

    public Task<bool> UpdateAsync(TaskItem task, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        lock (_lock)
        {
            var index = _tasks.FindIndex(t => t.Id == task.Id);
            if (index < 0) return Task.FromResult(false);

            _tasks[index] = task;
            return Task.FromResult(true);
        }
    }

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        lock (_lock)
        {
            var removed = _tasks.RemoveAll(t => t.Id == id) > 0;
            return Task.FromResult(removed);
        }
    }
}

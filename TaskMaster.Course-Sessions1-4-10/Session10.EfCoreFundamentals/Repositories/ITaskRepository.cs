using TaskMaster.Api.Models;

namespace TaskMaster.Api.Repositories;

/// <summary>
/// Persistence contract. It knows nothing about HTTP (no IActionResult here).
/// Two implementations: InMemoryTaskRepository (Week 2) and TaskRepository (EF Core + SQL Server, Session 10).
/// This interface did not change at all. That is the payoff of the repository pattern.
/// </summary>
public interface ITaskRepository
{
    Task<IReadOnlyList<TaskItem>> GetAllAsync(CancellationToken cancellationToken);

    Task<TaskItem?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task AddAsync(
        TaskItem task,
        CancellationToken cancellationToken);

    /// <returns>true if the task existed and was updated; false if not found.</returns>
    Task<bool> UpdateAsync(
        TaskItem task,
        CancellationToken cancellationToken);

    /// <returns>true if the task existed and was deleted; false if not found.</returns>
    Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken);
}

using TaskMaster.Api.Models;

namespace TaskMaster.Api.Repositories;

/// <summary>
/// Persistence contract. It knows nothing about HTTP (no IActionResult here).
/// Today: in-memory list. Week 3: EF Core + SQL Server.
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

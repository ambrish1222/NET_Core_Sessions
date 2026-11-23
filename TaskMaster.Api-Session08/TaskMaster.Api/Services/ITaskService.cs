using TaskMaster.Api.DTOs;

namespace TaskMaster.Api.Services;

/// <summary>
/// Application-layer contract. The controller depends on this abstraction,
/// never on the concrete TaskService.
/// </summary>
public interface ITaskService
{
    Task<IReadOnlyList<TaskResponse>> GetAllAsync(CancellationToken cancellationToken);

    Task<TaskResponse?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<TaskResponse> CreateAsync(CreateTaskRequest request, CancellationToken cancellationToken);

    /// <returns>true if updated; false if the task does not exist.</returns>
    Task<bool> UpdateAsync(int id, UpdateTaskRequest request, CancellationToken cancellationToken);

    /// <returns>true if deleted; false if the task does not exist.</returns>
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}

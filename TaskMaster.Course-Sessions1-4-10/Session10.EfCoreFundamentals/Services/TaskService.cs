using TaskMaster.Api.DTOs;
using TaskMaster.Api.Models;
using TaskMaster.Api.Repositories;

namespace TaskMaster.Api.Services;

/// <summary>
/// Application logic: validates rules, coordinates the repository, maps objects.
/// It has no HTTP concepts (no IActionResult, no status codes).
/// </summary>
public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<TaskResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        var tasks = await _repository.GetAllAsync(cancellationToken);
        return tasks.Select(Map).ToList();
    }

    public async Task<TaskResponse?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var task = await _repository.GetByIdAsync(id, cancellationToken);
        return task is null ? null : Map(task);
    }

    public async Task<TaskResponse> CreateAsync(CreateTaskRequest request, CancellationToken cancellationToken)
    {
        // 1. Validate application rules
        if (string.IsNullOrWhiteSpace(request.Title))
            throw new ArgumentException("Title is required.");

        if (request.DueDate.HasValue && request.DueDate.Value < DateTime.UtcNow)
            throw new ArgumentException("Due date cannot be in the past.");

        // 2. Map request -> domain model, setting server-controlled values
        var task = new TaskItem
        {
            Id = 0,                              // real Id is assigned by the repository
            Title = request.Title.Trim(),
            Priority = request.Priority,
            Status = TaskItemStatus.Pending,     // every new task starts as Pending
            DueDate = request.DueDate,
            CreatedAt = DateTime.UtcNow          // slide 17: stored in UTC
        };

        // 3. Persist
        await _repository.AddAsync(task, cancellationToken);

        // 4. Map domain model -> response (task.Id is now filled in)
        return Map(task);
    }

    public async Task<bool> UpdateAsync(int id, UpdateTaskRequest request, CancellationToken cancellationToken)
    {
        // 1. Load the existing task. Missing -> tell the controller (it will return 404)
        var existing = await _repository.GetByIdAsync(id, cancellationToken);
        if (existing is null) return false;

        // 2. Validate update rules BEFORE changing anything
        if (string.IsNullOrWhiteSpace(request.Title))
            throw new ArgumentException("Title is required.");

        if (existing.Status == TaskItemStatus.Completed && request.Status == TaskItemStatus.Pending)
            throw new ArgumentException("A completed task cannot be moved back to Pending.");

        // 3. Apply the changes
        existing.Title = request.Title.Trim();
        existing.Priority = request.Priority;
        existing.Status = request.Status;
        existing.DueDate = request.DueDate;

        // 4. Persist (the repository saves it: EF Core SaveChangesAsync)
        return await _repository.UpdateAsync(existing, cancellationToken);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        // Business rules (for example "cannot delete an in-progress task") would go here.
        return await _repository.DeleteAsync(id, cancellationToken);
    }

    // Manual mapping: domain model -> response DTO.
    private static TaskResponse Map(TaskItem task)
    {
        return new TaskResponse
        {
            Id = task.Id,
            Title = task.Title,
            Status = task.Status.ToString(),
            Priority = task.Priority.ToString(),
            DueDate = task.DueDate,
            CreatedAt = task.CreatedAt
        };
    }
}

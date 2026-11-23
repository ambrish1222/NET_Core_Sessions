using System.ComponentModel.DataAnnotations;
using TaskMaster.Api.Models;

namespace TaskMaster.Api.DTOs;

/// <summary>
/// Request body for PUT /api/tasks/{id}. The id comes from the route, not the body.
/// </summary>
public class UpdateTaskRequest
{
    [StringLength(100)]
    public string Title { get; init; } = string.Empty;

    [EnumDataType(typeof(TaskPriority))]
    public TaskPriority Priority { get; init; } = TaskPriority.Medium;

    [EnumDataType(typeof(TaskItemStatus))]
    public TaskItemStatus Status { get; init; } = TaskItemStatus.Pending;

    public DateTime? DueDate { get; init; }
}

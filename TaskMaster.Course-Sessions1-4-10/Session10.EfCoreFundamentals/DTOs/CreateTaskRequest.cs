using System.ComponentModel.DataAnnotations;
using TaskMaster.Api.Models;

namespace TaskMaster.Api.DTOs;

/// <summary>
/// Request body for POST /api/tasks.
/// Note what is NOT here: Id and Status. Those are server-controlled.
/// </summary>
public class CreateTaskRequest
{
    [StringLength(100)]
    public string Title { get; init; } = string.Empty;

    // Accepts "High" or 3 in JSON. Undefined values (for example 9) are rejected with 400.
    [EnumDataType(typeof(TaskPriority))]
    public TaskPriority Priority { get; init; } = TaskPriority.Medium;

    public DateTime? DueDate { get; init; }
}

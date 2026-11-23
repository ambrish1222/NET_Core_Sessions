namespace TaskMaster.Api.Models;

/// <summary>
/// The domain model: what we store. It is never sent directly to the client.
/// </summary>
public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public TaskItemStatus Status { get; set; } = TaskItemStatus.Pending;
    public DateTime? DueDate { get; set; }
}

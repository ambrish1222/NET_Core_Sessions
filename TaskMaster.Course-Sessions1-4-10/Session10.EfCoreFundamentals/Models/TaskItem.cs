namespace TaskMaster.Api.Models;

/// <summary>
/// Slides 6-7. An ENTITY: a C# class EF Core maps to the dbo.Tasks table.
/// Each property becomes a column; Id is the primary key by convention.
/// It is never sent directly to the client (the API uses DTOs).
/// </summary>
public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public TaskItemStatus Status { get; set; } = TaskItemStatus.Pending;
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public DateTime? DueDate { get; set; }
    public DateTime CreatedAt { get; set; }

    // Foreign key: nullable, so a task may belong to nobody yet (Session 9: Tasks.UserId -> Users.Id).
    public int? UserId { get; set; }

    // Navigation property: lets C# code walk from a task to its owner. Relationships get a full session later.
    public User? User { get; set; }
}

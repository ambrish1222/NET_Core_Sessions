using TaskMaster.Session04.Models;

namespace TaskMaster.Session04.Data;

/// <summary>Ten tasks with a mix of statuses and priorities, and one that is overdue.</summary>
public static class SampleData
{
    public static List<TaskItem> Create()
    {
        var today = DateTime.UtcNow.Date;

        return new List<TaskItem>
        {
            new TaskItem { Id = 1,  Title = "Set up the project",     Status = TaskItemStatus.Completed,  Priority = TaskPriority.High,   DueDate = today.AddDays(-5) },
            new TaskItem { Id = 2,  Title = "Design the model",       Status = TaskItemStatus.Completed,  Priority = TaskPriority.Medium, DueDate = today.AddDays(-2) },
            new TaskItem { Id = 3,  Title = "Write the repository",   Status = TaskItemStatus.InProgress, Priority = TaskPriority.High,   DueDate = today.AddDays(1) },
            new TaskItem { Id = 4,  Title = "Add LINQ queries",       Status = TaskItemStatus.Pending,    Priority = TaskPriority.High,   DueDate = today.AddDays(2) },
            new TaskItem { Id = 5,  Title = "Handle exceptions",      Status = TaskItemStatus.Pending,    Priority = TaskPriority.Medium, DueDate = today.AddDays(4) },
            new TaskItem { Id = 6,  Title = "Write unit tests",       Status = TaskItemStatus.Pending,    Priority = TaskPriority.Medium, DueDate = today.AddDays(-1) },
            new TaskItem { Id = 7,  Title = "Update documentation",   Status = TaskItemStatus.Pending,    Priority = TaskPriority.Low,    DueDate = today.AddDays(10) },
            new TaskItem { Id = 8,  Title = "Review code",            Status = TaskItemStatus.InProgress, Priority = TaskPriority.Low,    DueDate = today.AddDays(3) },
            new TaskItem { Id = 9,  Title = "Plan async work",        Status = TaskItemStatus.Pending,    Priority = TaskPriority.High,   DueDate = today.AddDays(6) },
            new TaskItem { Id = 10, Title = "Prepare the demo",       Status = TaskItemStatus.Completed,  Priority = TaskPriority.Low,    DueDate = today }
        };
    }
}

namespace TaskMaster.Session03.Domain;

/// <summary>Five ready-made tasks (homework asks for at least 5).</summary>
public static class SampleData
{
    public static List<TaskItem> Create()
    {
        var today = DateTime.UtcNow.Date;

        return new List<TaskItem>
        {
            new TaskItem { Id = 1, Title = "Learn C# basics", Description = "Variables, types, methods", Status = TaskItemStatus.Completed, Priority = TaskPriority.High, DueDate = today.AddDays(-3) },
            new TaskItem { Id = 2, Title = "Learn classes and objects", Status = TaskItemStatus.InProgress, Priority = TaskPriority.High, DueDate = today.AddDays(1) },
            new TaskItem { Id = 3, Title = "Model TaskItem", Description = "Properties, enums", Status = TaskItemStatus.Pending, Priority = TaskPriority.Medium, DueDate = today.AddDays(3) },
            new TaskItem { Id = 4, Title = "Create ITaskRepository", Status = TaskItemStatus.Pending, Priority = TaskPriority.Medium, DueDate = today.AddDays(5) },
            new TaskItem { Id = 5, Title = "Prepare for LINQ", Status = TaskItemStatus.Pending, Priority = TaskPriority.Low, DueDate = today.AddDays(7) }
        };
    }
}

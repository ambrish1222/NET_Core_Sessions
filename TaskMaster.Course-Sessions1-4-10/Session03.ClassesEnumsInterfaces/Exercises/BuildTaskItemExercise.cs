using TaskMaster.Session03.Domain;

namespace TaskMaster.Session03.Exercises;

/// <summary>Exercise 1 (slide 25): build TaskItem, the two enums and three different objects.</summary>
public static class BuildTaskItemExercise
{
    public static void Run()
    {
        var first = new TaskItem
        {
            Id = 1,
            Title = "Learn C#",
            Description = "Finish Week 1",
            Status = TaskItemStatus.Completed,
            Priority = TaskPriority.High,
            DueDate = DateTime.UtcNow.AddDays(-1)
        };

        var second = new TaskItem
        {
            Id = 2,
            Title = "Build the model",
            Status = TaskItemStatus.InProgress,
            Priority = TaskPriority.Medium,
            DueDate = DateTime.UtcNow.AddDays(2)
        };

        var third = new TaskItem
        {
            Id = 3,
            Title = "Write the repository",
            Description = "ITaskRepository + InMemoryTaskRepository",
            Status = TaskItemStatus.Pending,
            Priority = TaskPriority.Low,
            DueDate = DateTime.UtcNow.AddDays(7)
        };

        foreach (var task in new[] { first, second, third })
        {
            Console.WriteLine(task);
        }
    }
}

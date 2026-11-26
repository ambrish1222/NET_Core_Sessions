namespace TaskMaster.Session02.Homework;

/// <summary>
/// Homework (slide 27): validation and message helpers for tasks.
/// Priority scale in this session: 1 = High, 2 = Medium, 3 = Low.
/// </summary>
public static class TaskValidator
{
    public static bool IsValidTitle(string? title)
    {
        return !string.IsNullOrWhiteSpace(title);
    }

    public static bool IsValidPriority(int priority)
    {
        return priority >= 1 && priority <= 3;
    }

    public static string GetPriorityLabel(int priority)
    {
        return priority switch
        {
            1 => "High",
            2 => "Medium",
            3 => "Low",
            _ => "Unknown"
        };
    }

    public static string GetTaskMessage(bool isCompleted, int priority)
    {
        if (!IsValidPriority(priority))
        {
            return "Invalid priority.";
        }

        if (isCompleted)
        {
            return "Task completed. Well done!";
        }

        return priority == 1
            ? "HIGH priority and still pending: do this first!"
            : $"Pending ({GetPriorityLabel(priority)} priority).";
    }
}

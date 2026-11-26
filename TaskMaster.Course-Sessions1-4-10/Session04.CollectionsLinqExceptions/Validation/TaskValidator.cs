using TaskMaster.Session04.Models;

namespace TaskMaster.Session04.Validation;

/// <summary>Slides 24-27 and Exercise 3: throw when the caller breaks a method's contract.</summary>
public static class TaskValidator
{
    // Slide 24
    public static void ValidatePriority(int priority)
    {
        if (priority < 1 || priority > 3)
        {
            throw new ArgumentOutOfRangeException(nameof(priority), priority, "Priority must be 1 (Low), 2 (Medium) or 3 (High).");
        }
    }

    // Slide 27: guard clauses check bad input early, so the rest of the method stays simple
    public static void ValidateTask(TaskItem task)
    {
        if (task is null)
        {
            throw new ArgumentNullException(nameof(task));
        }

        if (string.IsNullOrWhiteSpace(task.Title))
        {
            throw new ArgumentException("Title is required.", nameof(task));
        }

        if (!Enum.IsDefined(task.Priority))
        {
            throw new ArgumentException($"Priority {(int)task.Priority} is not valid.", nameof(task));
        }
    }
}

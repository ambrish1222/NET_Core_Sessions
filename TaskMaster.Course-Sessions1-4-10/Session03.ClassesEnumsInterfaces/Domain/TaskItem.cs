namespace TaskMaster.Session03.Domain;

/// <summary>Slide 21-22: the Task API model.</summary>
public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TaskItemStatus Status { get; set; }
    public TaskPriority Priority { get; set; }
    public DateTime DueDate { get; set; }

    public override string ToString()
    {
        return $"#{Id} {Title} [{Status}, {Priority}] due {DueDate:yyyy-MM-dd}";
    }
}

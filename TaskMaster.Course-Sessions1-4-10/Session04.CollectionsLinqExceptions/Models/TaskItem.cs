namespace TaskMaster.Session04.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public TaskItemStatus Status { get; set; }
    public TaskPriority Priority { get; set; }
    public DateTime DueDate { get; set; }

    public override string ToString()
    {
        return $"#{Id,-2} {Title,-24} {Status,-10} {Priority,-6} due {DueDate:yyyy-MM-dd}";
    }
}

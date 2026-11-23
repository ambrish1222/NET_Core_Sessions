namespace TaskMaster.Api.DTOs;

/// <summary>
/// What the client receives. Status and Priority are readable text, not numbers.
/// </summary>
public class TaskResponse
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public string Priority { get; init; } = string.Empty;
    public DateTime? DueDate { get; init; }
}

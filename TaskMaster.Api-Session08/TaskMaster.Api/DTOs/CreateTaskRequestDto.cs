using System.ComponentModel.DataAnnotations;

namespace TaskMaster.Api.DTOs;

/// <summary>
/// What the client sends to create a task (the request body).
/// [ApiController] automatically returns 400 if these attributes are violated.
/// </summary>
public record CreateTaskRequestDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Title { get; init; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; init; }

    /// <summary>1 = Low, 2 = Medium, 3 = High</summary>
    [Range(1, 3)]
    public int Priority { get; init; } = 2;

    public DateTime? DueDate { get; init; }
}

namespace TaskMaster.Api.DTOs;

/// <summary>
/// What the client receives. Only the fields we choose to expose.
/// </summary>
public record TaskResponseDto(
    int Id,
    string Title,
    string Status,
    int Priority
);

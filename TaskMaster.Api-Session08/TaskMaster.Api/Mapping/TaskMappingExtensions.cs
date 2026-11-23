using TaskMaster.Api.DTOs;
using TaskMaster.Api.Models;

namespace TaskMaster.Api.Mapping;

public static class TaskMappingExtensions
{
    // 1. Transform Entity -> Response DTO
    public static TaskResponseDto ToDto(this TaskItem task)
    {
        return new TaskResponseDto(
            task.Id,
            task.Title,
            task.Status.ToString(),
            (int)task.Priority
        );
    }

    // 2. Transform Create DTO -> Entity
    public static TaskItem ToEntity(this CreateTaskRequestDto dto)
    {
        return new TaskItem
        {
            Title = dto.Title.Trim(),
            Description = dto.Description?.Trim(),
            Priority = (TaskPriority)dto.Priority,
            Status = TaskItemStatus.Pending,
            DueDate = dto.DueDate
        };
    }
}

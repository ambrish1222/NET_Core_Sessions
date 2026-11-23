namespace TaskMaster.Api.Models;

// NOTE: We call this TaskItemStatus (not TaskStatus) because .NET already has
// System.Threading.Tasks.TaskStatus. Using the same name causes error CS0104
// ("ambiguous reference") when ImplicitUsings is enabled.
public enum TaskItemStatus
{
    Pending,
    InProgress,
    Completed
}

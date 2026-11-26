namespace TaskMaster.Session03.Domain;

// The deck calls this enum "TaskStatus". We name it TaskItemStatus because .NET already has
// System.Threading.Tasks.TaskStatus, and the two clash (error CS0104) in projects that use namespaces.
public enum TaskItemStatus
{
    Pending,
    InProgress,
    Completed
}

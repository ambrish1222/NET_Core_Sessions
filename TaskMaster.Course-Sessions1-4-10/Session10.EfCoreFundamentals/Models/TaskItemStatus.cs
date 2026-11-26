namespace TaskMaster.Api.Models;

// Named TaskItemStatus (not TaskStatus) because System.Threading.Tasks.TaskStatus already exists (CS0104).
//
// Explicit numbers on purpose: EF Core stores an enum as its INTEGER value, so these numbers are what ends
// up in the Tasks.Status column. They match the Session 9 SQL codes: 1 = Pending, 2 = InProgress, 3 = Completed.
// Never reorder or renumber an enum that is already stored in a database.
public enum TaskItemStatus
{
    Pending = 1,
    InProgress = 2,
    Completed = 3
}
